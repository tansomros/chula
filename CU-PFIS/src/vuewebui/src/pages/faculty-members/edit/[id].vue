<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useReferenceDataStore } from '@/stores/referenceData'
import FacultyForm from '@/components/FacultyForm.vue'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const facultyStore = useFacultyMembersStore()
const refStore = useReferenceDataStore()

const submitting = ref(false)
const errorMessage = ref('')

const member = computed(() => facultyStore.currentDetail)

async function loadData() {
  if (!route.params.id) return
  await Promise.all([refStore.ensureLoaded(), facultyStore.fetchByExternalId(route.params.id)])
}

onMounted(loadData)
watch(() => route.params.id, loadData)

async function handleSubmit(payload) {
  submitting.value = true
  errorMessage.value = ''
  try {
    await facultyStore.updateMember(route.params.id, payload)
    await router.push(`/faculty-members/view/${route.params.id}`)
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <v-progress-linear v-if="facultyStore.loading && !member" indeterminate color="primary" class="mb-4" />

  <div v-if="member">
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn
          icon="mdi-arrow-left"
          variant="outlined"
          :to="`/faculty-members/view/${member.externalId}`"
        />
        <h1 class="text-h5 font-weight-bold">{{ t('faculty.editTitle') }}</h1>
      </div>
    </div>

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <FacultyForm
      :initial-value="member"
      :submitting="submitting"
      is-edit
      @submit="handleSubmit"
      @cancel="router.push(`/faculty-members/view/${member.externalId}`)"
    />
  </div>

  <v-alert v-else-if="!facultyStore.loading" type="warning" variant="tonal">
    {{ facultyStore.error || t('common.noData') }}
  </v-alert>
</template>
