<script setup>
import { ref, computed, onMounted, onUnmounted } from "vue";
import { motion, AnimatePresence } from "motion-v";

const props = defineProps({
  collapsedSize: {
    type: Object,
    default: () => ({ width: 124, height: 44 }),
  },
  borderRadius: { 
    type: [Number, String], 
    default: 32 
  },
  expandedBackground: { 
    type: String, 
    default: "var(--color-bg)" 
  },
  collapsedBackground: { 
    type: String, 
    default: null 
  },
  collapsedRadius: { 
    type: [Number, String], 
    default: null 
  },
  spring: {
    type: Object,
    default: () => ({ type: "spring", duration: 0.6, bounce: 0 }),
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
});

const expanded = defineModel({ type: Boolean, default: false });

const css = (v) => (typeof v === "number" ? `${v}px` : v);

const contentRef = ref(null);
const measuredWidth = ref(0);
const measuredHeight = ref(0);
let ro;

onMounted(() => {
  ro = new ResizeObserver(() => {
    const el = contentRef.value;
    if (!el) return;
    measuredWidth.value = el.offsetWidth;
    measuredHeight.value = el.offsetHeight;
  });
  if (contentRef.value) ro.observe(contentRef.value);
});

onUnmounted(() => ro && ro.disconnect());

const miniRef = ref(null);
const originBackground = ref(null);
const originRadius = ref(null);
const measuredCollapsed = ref(null);
let miniRo;

onMounted(() => {
  const host = miniRef.value?.$el ?? miniRef.value;
  const el = host?.firstElementChild ?? host;
  if (!(el instanceof HTMLElement)) return;

  const styles = window.getComputedStyle(el);
  originBackground.value = styles.backgroundColor;
  originRadius.value = styles.borderRadius;

  const measure = () => {
    measuredCollapsed.value = { width: el.offsetWidth, height: el.offsetHeight };
  };
  measure();
  miniRo = new ResizeObserver(measure);
  miniRo.observe(el);
});

onUnmounted(() => miniRo && miniRo.disconnect());

const collapsedDims = computed(() => measuredCollapsed.value ?? props.collapsedSize);

const collapsedBg = computed(
  () => props.collapsedBackground ?? originBackground.value ?? props.expandedBackground
);

const collapsedRad = computed(
  () => props.collapsedRadius ?? originRadius.value ?? css(props.borderRadius)
);

const animatedSize = computed(() =>
  expanded.value
    ? {
        width: `${measuredWidth.value}px`,
        height: `${measuredHeight.value}px`,
        backgroundColor: props.expandedBackground,
        borderRadius: css(props.borderRadius),
      }
    : {
        width: css(collapsedDims.value.width),
        height: css(collapsedDims.value.height),
        backgroundColor: collapsedBg.value,
        borderRadius: collapsedRad.value,
      }
);

const contentMin = computed(() => ({
  minWidth: measuredWidth.value ? `${measuredWidth.value}px` : undefined,
  minHeight: measuredHeight.value ? `${measuredHeight.value}px` : undefined,
}));

const miniTransition = computed(() =>
  expanded.value
    ? { duration: props.swapDuration * 0.5 }
    : { duration: props.swapDuration, delay: props.swapDelay }
);
const expandTransition = computed(() =>
  expanded.value
    ? { duration: props.swapDuration, delay: props.swapDelay }
    : { duration: props.swapDuration * 0.5 }
);

const open = () => (expanded.value = true);
const close = () => (expanded.value = false);

defineExpose({ open, close });
</script>

<template>
  <div
    class="morph-shell-placeholder"
    :style="{ width: css(collapsedDims.width), height: css(collapsedDims.height) }"
  >
    <motion.div
      class="morph-shell"
      :animate="animatedSize"
      :transition="spring"
      role="button"
      :tabindex="expanded ? -1 : 0"
      @click="!expanded && open()"
      @keydown.enter.prevent="!expanded && open()"
      @keydown.esc="expanded && close()"
    >
      <motion.div
        ref="miniRef"
        class="morph-mini"
        :animate="{ opacity: expanded ? 0 : 1, filter: expanded ? 'blur(2px)' : 'blur(0px)' }"
        :transition="miniTransition"
        :style="{ pointerEvents: expanded ? 'none' : 'auto' }"
      >
        <slot name="collapsed" :open="open" />
      </motion.div>

      <motion.div
        class="morph-expand"
        :animate="{ opacity: expanded ? 1 : 0, filter: expanded ? 'blur(0px)' : 'blur(2px)' }"
        :transition="expandTransition"
        :style="{ pointerEvents: expanded ? 'auto' : 'none' }"
        @click.stop
      >
        <div ref="contentRef" class="morph-content" :style="contentMin">
          <slot name="expanded" :close="close" />
        </div>
      </motion.div>
    </motion.div>

    <AnimatePresence>
      <motion.div
        v-if="backdrop && expanded"
        key="backdrop"
        class="morph-shell-backdrop"
        :initial="{ opacity: 0 }"
        :animate="{ opacity: 1 }"
        :exit="{ opacity: 0 }"
        :transition="spring"
        @click="close"
      />
    </AnimatePresence>
  </div>
</template>

<style scoped>
.morph-shell-placeholder {
  z-index: 700;
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.morph-shell {
  position: absolute;
  top:    var(--morph-top, 0);
  left:   var(--morph-left, 0);
  right:  var(--morph-right, auto);
  bottom: var(--morph-bottom, auto);
  overflow: hidden;
  cursor: pointer;
  outline: none;
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
  display: flex;
  align-items: center;
  justify-content: center;
}

.morph-content {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  padding: 16px;
}

.morph-shell-backdrop {
  z-index: -1;
  background: #0000004d;
  position: fixed;
  inset: 0;
}
</style>
