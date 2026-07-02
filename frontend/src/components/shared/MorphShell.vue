<script setup>
import { ref, computed, watch, nextTick, onUnmounted } from "vue";
import { motion, AnimatePresence } from "motion-v";

const props = defineProps({
  borderRadius: { 
    type: [Number, String], 
    default: 32 
  },
  expandedBackground: { 
    type: String, 
    default: "var(--color-bg)" 
  },
  spring: { 
    type: Object, 
    default: () => ({ type: "spring", duration: 0.6, bounce: 0 }) 
  },
  swapDuration: { 
    type: Number, 
    default: 0.3 
  },
  swapDelay: { 
    type: Number, 
    default: 0.25 
  },
  backdrop: { 
    type: Boolean, 
    default: true 
  },
  offset: { 
    type: Number, 
    default: 8 
  },
});

const expanded = defineModel({ type: Boolean, default: false });
const css = (v) => (typeof v === "number" ? `${v}px` : v);

const originRef  = ref(null);
const contentRef = ref(null);

const origin  = ref(null); 
const size    = ref(null); 
const visible = ref(false);

let ro = null;
let closeTimer = null;
const closeMs = computed(() => (props.spring?.duration ?? 0.6) * 1000);

function readOrigin() {
  const host = originRef.value?.$el ?? originRef.value;
  const el = host?.firstElementChild ?? host;
  if (!(el instanceof HTMLElement)) return null;
  const r = el.getBoundingClientRect();
  const s = window.getComputedStyle(el);
  return {
    top: r.top, left: r.left, width: r.width, height: r.height,
    borderRadius: s.borderRadius,
    background: s.backgroundColor,
    boxShadow: s.boxShadow,
  };
}

function measure() {
  const el = contentRef.value;
  if (el) size.value = { 
    width: el.offsetWidth, 
    height: el.offsetHeight 
  };
}

watch(expanded, (val) => (val ? runOpen() : runClose()));

async function runOpen() {
  if (closeTimer) { 
    clearTimeout(closeTimer); 
    closeTimer = null; 
  }

  size.value = null;
  origin.value = readOrigin();
  visible.value = true;

  await nextTick();

  requestAnimationFrame(() => {
    measure();
    if (ro) ro.disconnect();
    ro = new ResizeObserver(measure);
    if (contentRef.value) ro.observe(contentRef.value);
  });

  window.addEventListener("keydown", onKey);
}

function runClose() {
  if (!visible.value) return;

  window.removeEventListener("keydown", onKey);

  if (ro) { 
    ro.disconnect(); 
    ro = null; 
  }

  closeTimer = window.setTimeout(() => {
    visible.value = false;
    closeTimer = null;
  }, closeMs.value);
}

function onKey(e) { if (e.key === "Escape") close(); }

function open()  { expanded.value = true; }
function close() { expanded.value = false; }

const target = computed(() => {
  if (!origin.value || !size.value) return null;

  const vw = window.innerWidth, vh = window.innerHeight, safe = 8;

  const w = size.value.width, h = size.value.height;

  const r = origin.value;

  const clamp = (v, min, max) => Math.min(max, Math.max(min, v));

  const left = r.left < vw / 2 ? r.left - props.offset : r.left + r.width  + props.offset - w;

  const top  = r.top  < vh / 2 ? r.top  - props.offset : r.top  + r.height + props.offset - h;

  return {
    left: Math.round(clamp(left, safe, Math.max(safe, vw - w - safe))),
    top:  Math.round(clamp(top,  safe, Math.max(safe, vh - h - safe))),
    width: w, height: h,
  };
});

const collapsedPose = computed(() => origin.value ? {
  top: origin.value.top, left: origin.value.left,
  width: origin.value.width, height: origin.value.height,
  borderRadius: origin.value.borderRadius,
  backgroundColor: origin.value.background,
  boxShadow: origin.value.boxShadow,
} : {});

const expandedPose = computed(() => target.value ? {
  top: target.value.top, left: target.value.left,
  width: target.value.width, height: target.value.height,
  borderRadius: css(props.borderRadius),
  backgroundColor: props.expandedBackground,
  boxShadow: "0 12px 40px #00000026",
} : collapsedPose.value);

const isOpen = computed(() => expanded.value && !!size.value);
const boxAnimate = computed(() => isOpen.value ? expandedPose.value : collapsedPose.value);

const miniAnimate = computed(() => ({
  opacity: isOpen.value ? 0 : 1,
  filter:  isOpen.value ? "blur(2px)" : "blur(0px)",
  transition: isOpen.value
    ? { duration: props.swapDuration * 0.5 }
    : { duration: props.swapDuration, delay: props.swapDelay },
}));

const expandAnimate = computed(() => ({
  opacity: isOpen.value ? 1 : 0,
  filter:  isOpen.value ? "blur(0px)" : "blur(2px)",
  transition: isOpen.value
    ? { duration: props.swapDuration, delay: props.swapDelay }
    : { duration: props.swapDuration * 0.5 },
}));

onUnmounted(() => {
  if (ro) ro.disconnect();
  if (closeTimer) clearTimeout(closeTimer);
  window.removeEventListener("keydown", onKey);
});

defineExpose({ open, close });
</script>

<template>
  <span
    ref="originRef"
    class="morph-origin"
    :style="{ visibility: expanded ? 'hidden' : 'visible' }"
  >
    <slot name="collapsed" :open="open" />
  </span>

  <Teleport to="body">
    <AnimatePresence>
      <motion.div
        v-if="backdrop && isOpen"
        key="morph-backdrop"
        class="morph-backdrop"
        :initial="{ opacity: 0 }" :animate="{ opacity: 1 }" :exit="{ opacity: 0 }"
        :transition="spring"
        @click="close"
      />
    </AnimatePresence>

    <motion.div
      v-if="visible"
      class="morph-shell"
      :initial="collapsedPose"
      :animate="boxAnimate"
      :transition="spring"
    >
      <motion.div
        class="morph-mini"
        :initial="{ opacity: 1, filter: 'blur(0px)' }"
        :animate="miniAnimate"
        :style="{ pointerEvents: isOpen ? 'none' : 'auto' }"
      >
        <slot name="collapsed" :open="open" />
      </motion.div>

      <motion.div
        class="morph-expand"
        :initial="{ opacity: 0, filter: 'blur(2px)' }"
        :animate="expandAnimate"
        :style="{ pointerEvents: isOpen ? 'auto' : 'none' }"
        @click.stop
      >
        <div ref="contentRef" class="morph-content">
          <slot name="expanded" :close="close" />
        </div>
      </motion.div>
    </motion.div>
  </Teleport>
</template>

<style scoped>
.morph-origin {
  display: inline-flex;
}

.morph-shell {
  position: fixed;
  z-index: 1000;
  overflow: hidden;
}

@media (prefers-color-scheme: dark) {
  .morph-shell {
    box-shadow: 0 0 0 1px #ffffff0d !important;
  }
}

.morph-mini {
  position: absolute;
  inset: 0;
  display: flex;
  align-items: center;
  justify-content: center;
}

.morph-expand {
  position: absolute;
  inset: 0;
}

.morph-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: max-content;
  max-width: calc(100vw - 16px);
  display: flex;
  flex-direction: column;
  padding: 16px;
}

.morph-backdrop {
  position: fixed;
  inset: 0;
  z-index: 999;
  background: #0000004d;
}
</style>
