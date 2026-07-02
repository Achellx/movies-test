<script setup>
import { reactive, watch } from "vue";

const props = defineProps({
    initialValues: {
        type: Object,
        default: null
    },
    title: {
        type: String,
        default: "Add New Movie"
    },
    directors: {
        type: Array,
        default: () => []
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
    releaseYear: null,
    genre: '',
    hours: 0,
    minutes: 0,
    fKDirector: null
})

const errors = reactive({});

const pad = (n) => String(n).padStart(2, '0')
const buildDuration = (h, m) => `${pad(h)}:${pad(m)}:00`

function parseDuration(value) {
    if (!value) return { h: 0, m: 0 };
    const [h, m] = value.split(':').map(Number);
    return { h: Number(h), m: Number(m) };
}

// Edit mode
watch(() => props.initialValues,
    (movie) => {
        if (!movie) return;
        const { h, m } = parseDuration(movie.duration);
        form.name = movie.name ?? ''
        form.releaseYear = movie.releaseYear ?? null
        form.genre = movie.genre ?? ''
        form.hours = h
        form.minutes = m
        form.fKDirector = movie.fkDirector ?? null
    },
    {immediate: true}
)

watch(() => props.active, (isOpen) => {
    if (!isOpen) {
        clearErrors();

        if (!props.initialValues) {
            Object.assign(form, {
                name: '',
                releaseYear: null,
                genre: '',
                hours: 0,
                minutes: 0,
                fKDirector: null
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

    const year = Number(form.releaseYear);
    if (!Number.isFinite(year) || year < 1888 || year > 2100) {
        errors.releaseYear = 'Release year must be a valid year between 1888 and 2100';
    }

    if (!form.genre.trim()) errors.genre = 'Genre is mandatory';

    if (form.hours === 0 && form.minutes ===0 ) {
        errors.duration = 'Duration is mandatory';
    } else if (form.hours < 0 || form.hours > 23 || form.minutes < 0 || form.minutes > 59) {
        errors.duration = 'Duration must be a valid time (0-23 hours, 0-59 minutes)';
    }

    if (!form.fKDirector) errors.fKDirector = 'Director is mandatory';

    return Object.keys(errors).length === 0;
}

function handleSubmit() {
    if (props.submitting) return;
    if (!validate()) return;

    emit('submit', {
        name: form.name.trim(),
        releaseYear: Number(form.releaseYear),
        genre: form.genre.trim(),
        duration: buildDuration(form.hours, form.minutes),
        fKDirector: form.fKDirector
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
                v-model="form.releaseYear"
                type="text"
                placeholder="Release Year"
                :disabled="submitting"
                :data-error="errors.releaseYear ? 'true' : null" 
                @input="clearError('releaseYear')"
                @focus="clearError('releaseYear')"
            />
        </div>

        <div class="input-box">
            <input
                v-model="form.genre"
                type="text"
                placeholder="Genre"
                :disabled="submitting"
                :data-error="errors.genre ? 'true' : null" 
                @input="clearError('genre')"
                @focus="clearError('genre')"
            />
        </div>

        <div class="input-box simple-container direction-row gap-8">
            <input
                v-model="form.hours"
                type="number"
                min="0"
                max="23"
                placeholder="Hours"
                :disabled="submitting"
                :data-error="errors.duration ? 'true' : null" 
                @input="clearError('duration')"
                @focus="clearError('duration')"
            />
            <input
                v-model="form.minutes"
                type="number"
                min="0"
                max="59"
                placeholder="Minutes"
                :disabled="submitting"
                :data-error="errors.duration ? 'true' : null" 
                @input="clearError('duration')"
                @focus="clearError('duration')"
            />
        </div>

        <div class="input-box">
            <select
                v-model="form.fKDirector"
                :disabled="submitting"
                :data-error="errors.fKDirector ? 'true' : null" 
                @change="clearError('fKDirector')"
                @focus="clearError('fKDirector')"
            >
                <option :value="null" disabled>Select Director</option>
                <option 
                    v-for="director in directors"
                    :key="director.id"
                    :value="director.id"
                >
                    {{ director.name }}
                </option>
            </select>
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