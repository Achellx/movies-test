<script setup>
import { reactive, watch } from "vue";

const props = defineProps({
    initialValues: {
        type: Object,
        default: null
    },
    title: {
        type: String,
        default: "Add New Director"
    },
    submitLabel: {
        type: String,
        default: "Add"
    },
    submitting: {
        type: Boolean,
        default: false
    },
    active: {
        type: Boolean,
        default: true
    }
})

const emit = defineEmits(['submit', 'cancel']);

const form = reactive({
    name: '',
    nationality: '',
    age: null
})

const errors = reactive({});

// Edit mode
watch(() => props.initialValues,
    (director) => {
        if (!director) return;
        form.name = director.name ?? ''
        form.nationality = director.nationality ?? ''
        form.age = director.age ?? null
    },
    {immediate: true}
)

watch(() => props.active, (isOpen) => {
    if (!isOpen) {
        clearErrors();

        if (!props.initialValues) {
            Object.assign(form, {
                name: '',
                nationality: '',
                age: null
            });
        }
    }
})

function clearError(field) {
    delete errors[field];
}

function clearErrors() {
    Object.keys(errors).forEach(key => delete errors[key]);
}

function validate() {
    clearErrors();

    if (!form.name.trim()) errors.name = 'Name is mandatory';

    if (!form.nationality.trim()) errors.nationality = 'Nationality is mandatory';

    if (form.age === null || form.age === '') {
        errors.age = 'Age is mandatory';
    } else { 
        const age = Number(form.age);
        if (age <= 0 || age > 130 || !Number.isFinite(age)) {
            errors.age = 'Age must be a valid number between 0 and 130';
        }
    }
    
    return Object.keys(errors).length === 0;
}

function handleSubmit() {
    if (props.submitting) return;
    if (!validate()) return;

    emit('submit', {
        name: form.name.trim(),
        nationality: form.nationality.trim(),
        age: Number(form.age)
    })
}

</script>

<template>
    <form
        class="simple-container direction-column gap-16"
        autocomplete="off"
        novalidate
        @submit.prevent="handleSubmit"
    >
        <span class="display-large weight-500 padding-8 outline-text">{{ title }}</span>

        <div class="input-box">
            <input
                v-model="form.name"
                type="text"
                placeholder="Name"
                :disabled="submitting"
                :data-error="errors.name ? 'true' : null" 
                @input="clearError('name')"
                @focus="clearError('name')"
            />
        </div>

        <div class="input-box">
            <input
                v-model="form.nationality"
                type="text"
                placeholder="Nationality"
                :disabled="submitting"
                :data-error="errors.nationality ? 'true' : null" 
                @input="clearError('nationality')"
                @focus="clearError('nationality')"
            />
        </div>

        <div class="input-box">
            <input
                v-model="form.age"
                type="number"
                placeholder="Age"
                :disabled="submitting"
                :data-error="errors.age ? 'true' : null" 
                @input="clearError('age')"
                @focus="clearError('age')"
            />
        </div>

        <div class="buttons">
            <button
                type="button"
                class="secondary"
                :disabled="submitting"
                @click="$emit('cancel')"
            >
                <span>Cancel</span>
            </button>
            <button
                type="submit"
                class="primary"
                :disabled="submitting"
                :class="{'animation-loading-1' : submitting }"
            >
                <span>{{ submitLabel }}</span>
            </button>
        </div>
    </form>

</template>

<style scoped>
form {
    cursor: default;
}

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
    transition: all .2s;
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

button:hover {
    filter: brightness(0.85);
}

button:active {
    transform: scale(0.97);
}

.buttons {
    display: flex;
    gap: 8px;
    width: 100%;
}

.input-box {
    position: relative;
}

input, select {
    background: var(--color-bg);
    text-align: center;
    border-radius: 12px;
    width: 100%;
    padding: 12px;
    letter-spacing: -.4px;
    border: none;
    outline: none;
    font-size: 15px;
    box-shadow: 0 0 0 .5px #0000000d, 0 .5px 2.5px #0000001f;
    color: var(--color-text);
    user-select: none;
    transition: all .3s;
}

@media (prefers-color-scheme: dark) {
    input, select {
        box-shadow: 0 0 0 .5px #2a2a2a, 0 .5px 2.5px #00000029;
    }
}

select {
    border-right: 12px solid transparent;
}

input[data-error="true"],
select[data-error="true"] {
    box-shadow: 0 0 0 3px color-mix(in oklch, oklch(0.6 0.2 25) 25%, transparent);
    animation: field-shake 0.3s;
}

@keyframes field-shake {
  0%, 100% { transform: translateX(0); }
  20%, 60% { transform: translateX(-5px); }
  40%, 80% { transform: translateX(5px); }
}
</style>