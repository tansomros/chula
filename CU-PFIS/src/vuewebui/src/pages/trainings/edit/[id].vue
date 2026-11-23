<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useTrainingsStore } from '@/stores/trainings'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const trainingsStore = useTrainingsStore()

const detail = computed(() => trainingsStore.currentDetail)
const formRef = ref(null)
const submitting = ref(false)
const errorMessage = ref('')

const form = reactive({
  workType: 'Academic',
  titleTh: '',
  titleEn: '',
  sessionTopicTh: '',
  sessionTopicEn: '',
  organizerAndVenueTh: '',
  organizerAndVenueEn: '',
  startDate: '',
  endDate: '',
  requestedAmount: 0,
  bankName: '',
  bankAccountNumber: '',
  bankAccountHolderName: '',
})

const workTypeOptions = computed(() => [
  { value: 'Academic', title: t('training.workType.Academic') },
  { value: 'Research', title: t('training.workType.Research') },
  { value: 'Other', title: t('training.workType.Other') },
])

const isAcademicFunded = computed(() => detail.value?.category === 'AcademicFunded')
const requiredRule = (v) => Boolean(v && String(v).trim()) || t('common.required')

function hydrate(d) {
  if (!d) return
  form.workType = d.workType
  form.titleTh = d.titleTh || ''
  form.titleEn = d.titleEn || ''
  form.sessionTopicTh = d.sessionTopicTh || ''
  form.sessionTopicEn = d.sessionTopicEn || ''
  form.organizerAndVenueTh = d.organizerAndVenueTh || ''
  form.organizerAndVenueEn = d.organizerAndVenueEn || ''
  form.startDate = d.startDate || ''
  form.endDate = d.endDate || ''
  form.requestedAmount = d.requestedAmount || 0
  form.bankName = d.bankName || ''
  form.bankAccountNumber = d.bankAccountNumber || ''
  form.bankAccountHolderName = d.bankAccountHolderName || ''
}

async function loadData() {
  if (!route.params.id) return
  const d = await trainingsStore.fetchByExternalId(route.params.id)
  hydrate(d)
}

onMounted(loadData)
watch(() => route.params.id, loadData)

async function handleSave(resubmit) {
  const { valid } = await formRef.value.validate()
  if (!valid) return

  submitting.value = true
  errorMessage.value = ''
  try {
    await trainingsStore.updateTraining(route.params.id, {
      workType: form.workType,
      titleTh: form.titleTh.trim(),
      titleEn: form.titleEn.trim() || null,
      sessionTopicTh: form.sessionTopicTh.trim() || null,
      sessionTopicEn: form.sessionTopicEn.trim() || null,
      organizerAndVenueTh: form.organizerAndVenueTh.trim(),
      organizerAndVenueEn: form.organizerAndVenueEn.trim() || null,
      startDate: form.startDate,
      endDate: form.endDate,
      requestedAmount: isAcademicFunded.value ? Number(form.requestedAmount) || 0 : 0,
      bankName: isAcademicFunded.value ? form.bankName.trim() || null : null,
      bankAccountNumber: isAcademicFunded.value ? form.bankAccountNumber.trim() || null : null,
      bankAccountHolderName: isAcademicFunded.value ? form.bankAccountHolderName.trim() || null : null,
      resubmit,
    })
    await router.push(`/trainings/view/${route.params.id}`)
  } catch (err) {
    errorMessage.value = err.message || t('common.apiError')
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <v-progress-linear v-if="trainingsStore.loading && !detail" indeterminate color="primary" class="mb-4" />

  <div v-if="detail">
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" :to="`/trainings/view/${detail.externalId}`" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('training.editTitle') }}</h1>
          <div class="text-caption cu-muted">{{ t(`training.category.${detail.category}`) }}</div>
        </div>
      </div>
    </div>

    <v-alert v-if="detail.revisionNote" type="warning" variant="tonal" class="mb-4">
      <strong>{{ t('training.revisionReasonLabel') }}:</strong> {{ detail.revisionNote }}
    </v-alert>

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <v-form ref="formRef" @submit.prevent="handleSave(true)">
      <v-card class="pa-6 mb-5">
        <v-row>
          <v-col cols="12" md="4">
            <v-select v-model="form.workType" :items="workTypeOptions" :label="t('training.workTypeLabel')" />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.startDate" :label="t('training.startDate')" type="date" :rules="[requiredRule]" />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.endDate" :label="t('training.endDate')" type="date" :rules="[requiredRule]" />
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field v-model="form.titleTh" :label="t('training.titleTh')" :rules="[requiredRule]" />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="form.titleEn" :label="t('training.titleEn')" />
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field v-model="form.organizerAndVenueTh" :label="t('training.organizerTh')" :rules="[requiredRule]" />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="form.organizerAndVenueEn" :label="t('training.organizerEn')" />
          </v-col>

          <v-col v-if="isAcademicFunded" cols="12" md="4">
            <v-text-field v-model.number="form.requestedAmount" :label="t('training.requestedAmount')" type="number" min="0" />
          </v-col>
          <v-col v-if="isAcademicFunded && detail.canViewBankAccount" cols="12" md="4">
            <v-text-field v-model="form.bankName" :label="t('training.bankName')" />
          </v-col>
          <v-col v-if="isAcademicFunded && detail.canViewBankAccount" cols="12" md="4">
            <v-text-field v-model="form.bankAccountNumber" :label="t('training.bankAccountNumber')" />
          </v-col>
        </v-row>
      </v-card>

      <div class="d-flex justify-end ga-3">
        <v-btn variant="outlined" :disabled="submitting" :to="`/trainings/view/${detail.externalId}`">
          {{ t('common.cancel') }}
        </v-btn>
        <v-btn variant="tonal" color="secondary" :loading="submitting" @click="handleSave(false)">
          {{ t('common.save') }}
        </v-btn>
        <v-btn color="primary" type="submit" :loading="submitting" prepend-icon="mdi-send-outline">
          {{ t('training.resubmit') }}
        </v-btn>
      </div>
    </v-form>
  </div>

  <v-alert v-else-if="!trainingsStore.loading" type="warning" variant="tonal">
    {{ trainingsStore.error || t('common.noData') }}
  </v-alert>
</template>
