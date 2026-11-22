<script setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import FacultyForm from '@/components/FacultyForm.vue'

const { t } = useI18n()
const router = useRouter()
const facultyStore = useFacultyMembersStore()

const submitting = ref(false)
const errorMessage = ref('')

async function handleSubmit(payload) {
  submitting.value = true
  errorMessage.value = ''
  try {
    const createdExternalId = await facultyStore.createMember(payload)
    await router.push(`/faculty-members/view/${createdExternalId}`)
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/faculty-members/list" />
        <h1 class="text-h5 font-weight-bold">{{ t('faculty.createTitle') }}</h1>
      </div>
    </div>

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <FacultyForm
      :submitting="submitting"
      @submit="handleSubmit"
      @cancel="router.push('/faculty-members/list')"
    />
  </div>
</template>
