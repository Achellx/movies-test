<script setup>

const props = defineProps({
    title: {
        type: String,
        default: "Delete Registry",
    },
    message: {
        type: String,
        default: "Are you sure you want to delete this registry?",
    },
    confirmLabel: {
        type: String,
        default: "Confirm",
    },
    cancelLabel: {
        type: String,
        default: "Cancel",
    },
    danger: {
        type: Boolean,
        default: false,
    },
    busy: {
        type: Boolean,
        default: false,
    },
})

const emit = defineEmits(["confirm", "cancel"])

function onConfirm() {
    emit("confirm")
}

function onCancel() {
    emit("cancel")
}

</script>

<template>
    <div class="simple-container direction-column gap-24">
        <div class="simple-container direction-column gap-16">
            <span class="display-large weight-600 line-height-1">{{ title }}</span>
            <span class="body outline-text">{{ message }}</span>
        </div>

        <div class="simple-container gap-8">
            <button
                type="button"
                class="secondary"
                :disabled="busy"
                @click="onCancel"
            >
                {{ cancelLabel }}
            </button>
            <button
                type="button"
                :class="danger ? 'danger-true' : 'primary'"
                :disabled="busy"
                @click="onConfirm"
            >
                {{ confirmLabel }}
            </button>
        </div>
    </div>
</template>

<style scoped>
button {
    position: relative;
    display: inline-flex;
    flex: 1;
    align-items: center;
    justify-content: center;
    padding: 14px 16px;
    cursor: pointer;
    border-radius: 64px;
    font-size: 15px;
    font-weight: 500;
    width: max-content;
    border: none;
    outline: none;
    background: var(--color-surface);
    transition: all .3s;
    will-change: transform, filter;
}

button.secondary {
    background: var(--color-surface);
    color: var(--color-text-muted);
    border: 1px solid var(--color-surface);
    box-shadow: inset 0 1.5px #fff6;
}

button.primary {
    background: var(--color-12);
    color: var(--color-primary-text);
    border: 1px solid var(--color-12);
    box-shadow: inset 0 1.5px #fff3;
}

button:not(.danger-true):hover {
    filter: brightness(0.85);
}

button:active {
    transform: scale(0.97);
}

button.danger-true {
    background: var(--color-danger);
    box-shadow: inset 0 1.5px oklch(0.75 0.14 25 / 0.55);
    border: 1px solid var(--color-danger);
    color: var(--color-danger-text);
}

button.danger-true:hover {
    filter: saturate(1.15);
}
</style>