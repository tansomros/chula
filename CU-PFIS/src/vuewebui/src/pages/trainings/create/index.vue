<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useTrainingsStore } from '@/stores/trainings'
import { useLocalizedField } from '@/composables/useLocalizedField'

const { t } = useI18n()
const router = useRouter()
const authStore = useAuthStore()
const facultyStore = useFacultyMembersStore()
const trainingsStore = useTrainingsStore()
const { formatFullName } = useLocalizedField()

const formRef = ref(null)
const submitting = ref(false)
const errorMessage = ref('')

const today = new Date().toISOString().slice(0, 10)

const form = reactive({
  facultyMemberExternalIds: [],
  category: 'AcademicFunded',
  workType: 'Academic',
  titleTh: '',
  titleEn: '',
  sessionTopicTh: '',
  sessionTopicEn: '',
  organizerAndVenueTh: '',
  organizerAndVenueEn: '',
  startDate: today,
  endDate: today,
  requestedAmount: 0,
  bankName: '',
  bankAccountNumber: '',
  bankAccountHolderName: '',
})

const isStaffOrAdmin = computed(() => authStore.isAdmin || authStore.isStaff)
const isDirectRecordCategory = computed(
  () => form.category === 'FacultyOrganized' || form.category === 'ShortTermOrOverseas',
)
const isAcademicFunded = computed(() => form.category === 'AcademicFunded')

const derivedFiscalYearBe = computed(() => {
  if (!form.startDate) return 2570
  const [y, m] = form.startDate.split('-').map(Number)
  if (!y || !m) return 2570
  return (m >= 10 ? y + 1 : y) + 543
})

const categoryOptions = computed(() => {
  const opts = [
    { value: 'AcademicFunded', title: t('training.category.AcademicFunded') },
    { value: 'NonAcademicFunded', title: t('training.category.NonAcademicFunded') },
    { value: 'ShortTermOrOverseas', title: t('training.category.ShortTermOrOverseas') },
  ]
  if (isStaffOrAdmin.value) {
    opts.splice(2, 0, { value: 'FacultyOrganized', title: t('training.category.FacultyOrganized') })
  }
  return opts
})

const workTypeOptions = computed(() => [
  { value: 'Academic', title: t('training.workType.Academic') },
  { value: 'Research', title: t('training.workType.Research') },
  { value: 'Other', title: t('training.workType.Other') },
])

const facultyOptions = computed(() =>
  facultyStore.items.map((f) => ({
    value: f.externalId,
    title: `${f.personnelCode} — ${formatFullName(f, true)}`,
  })),
)

const selectedFacultyBudget = computed(() => {
  const targetId = form.facultyMemberExternalIds[0] || authStore.resolvedFacultyExternalId
  if (!targetId || !trainingsStore.budgetSummary?.items) return null
  return trainingsStore.budgetSummary.items.find((x) => x.facultyMemberExternalId === targetId) || null
})

const willBorrowNextYear = computed(() => {
  if (!isAcademicFunded.value || !selectedFacultyBudget.value) return false
  return Number(form.requestedAmount || 0) > Math.max(0, selectedFacultyBudget.value.remainingInYear)
})

const requiredRule = (v) => Boolean(v && String(v).trim()) || t('common.required')
const nonEmptyArrayRule = (v) => (Array.isArray(v) && v.length > 0) || t('common.required')

async function loadPrerequisites() {
  await Promise.all([
    facultyStore.fetchList(),
    trainingsStore.fetchFacultyBudgets(derivedFiscalYearBe.value),
  ])

  if (authStore.isFaculty && authStore.resolvedFacultyExternalId) {
    form.facultyMemberExternalIds = [authStore.resolvedFacultyExternalId]
  } else if (form.facultyMemberExternalIds.length === 0 && facultyStore.items.length > 0) {
    form.facultyMemberExternalIds = [facultyStore.items[0].externalId]
  }
}

onMounted(loadPrerequisites)
watch(derivedFiscalYearBe, (fy) => trainingsStore.fetchFacultyBudgets(fy))

async function submitForm(submitImmediately) {
  const { valid } = await formRef.value.validate()
  if (!valid) return

  submitting.value = true
  errorMessage.value = ''
  try {
    const createdIds = await trainingsStore.createTraining({
      facultyMemberExternalIds: form.facultyMemberExternalIds,
      category: form.category,
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
      submitImmediately,
    })

    if (createdIds.length === 1) {
      await router.push(`/trainings/view/${createdIds[0]}`)
    } else {
      await router.push('/trainings/list')
    }
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
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/trainings/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('training.createTitle') }}</h1>
          <div class="text-caption cu-muted">
            {{ t('training.fiscalYear') }}: <strong>{{ derivedFiscalYearBe }}</strong>
          </div>
        </div>
      </div>
    </div>

    <v-alert v-if="errorMessage" type="error" variant="tonal" class="mb-4">
      {{ errorMessage }}
    </v-alert>

    <!-- Live Fiscal Year Budget Snapshot Card -->
    <v-card v-if="isAcademicFunded && selectedFacultyBudget" variant="outlined" class="pa-4 mb-5 bg-surface-variant">
      <div class="text-subtitle-2 font-weight-bold mb-2">
        {{ t('training.budgetSnapshot', { fy: derivedFiscalYearBe }) }}
      </div>
      <div class="d-flex flex-wrap ga-6 text-body-2">
        <div>
          <span class="cu-muted">{{ t('training.annualQuota') }}:</span>
          <strong class="ml-1">{{ selectedFacultyBudget.annualQuota.toLocaleString() }}</strong>
        </div>
        <div>
          <span class="cu-muted">{{ t('training.consumed') }}:</span>
          <strong class="ml-1">{{ selectedFacultyBudget.consumedInYear.toLocaleString() }}</strong>
        </div>
        <div>
          <span class="cu-muted">{{ t('training.remaining') }}:</span>
          <strong class="ml-1 text-success">{{ selectedFacultyBudget.remainingInYear.toLocaleString() }}</strong>
        </div>
        <div>
          <span class="cu-muted">{{ t('training.borrowableNext') }}:</span>
          <strong class="ml-1 text-primary">{{ selectedFacultyBudget.borrowableFromNextYear.toLocaleString() }}</strong>
        </div>
      </div>
      <v-alert v-if="willBorrowNextYear" type="warning" variant="tonal" density="compact" class="mt-3">
        {{ t('training.borrowAlert') }}
      </v-alert>
    </v-card>

    <v-form ref="formRef" @submit.prevent="submitForm(true)">
      <v-card class="pa-6 mb-5">
        <v-row>
          <v-col cols="12" md="6">
            <v-select
              v-model="form.category"
              :items="categoryOptions"
              :label="t('training.categoryLabel')"
              :rules="[requiredRule]"
              data-testid="training-form-category"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-select
              v-model="form.workType"
              :items="workTypeOptions"
              :label="t('training.workTypeLabel')"
              :rules="[requiredRule]"
            />
          </v-col>

          <v-col cols="12">
            <v-select
              v-model="form.facultyMemberExternalIds"
              :items="facultyOptions"
              :label="t('training.facultySelect')"
              :multiple="isDirectRecordCategory && isStaffOrAdmin"
              :chips="isDirectRecordCategory && isStaffOrAdmin"
              :disabled="authStore.isFaculty"
              :hint="isDirectRecordCategory ? t('training.facultyMultiHint') : ''"
              persistent-hint
              :rules="[nonEmptyArrayRule]"
              @update:model-value="(val) => (form.facultyMemberExternalIds = Array.isArray(val) ? val : [val])"
            />
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.titleTh"
              :label="t('training.titleTh')"
              :rules="[requiredRule]"
              data-testid="training-form-title-th"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="form.titleEn" :label="t('training.titleEn')" />
          </v-col>

          <template v-if="form.category === 'FacultyOrganized'">
            <v-col cols="12" md="6">
              <v-text-field v-model="form.sessionTopicTh" :label="t('training.sessionTopicTh')" />
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field v-model="form.sessionTopicEn" :label="t('training.sessionTopicEn')" />
            </v-col>
          </template>

          <v-col cols="12" md="6">
            <v-text-field
              v-model="form.organizerAndVenueTh"
              :label="t('training.organizerTh')"
              :rules="[requiredRule]"
              data-testid="training-form-organizer-th"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="form.organizerAndVenueEn" :label="t('training.organizerEn')" />
          </v-col>

          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.startDate"
              :label="t('training.startDate')"
              type="date"
              :rules="[requiredRule]"
            />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="form.endDate"
              :label="t('training.endDate')"
              type="date"
              :rules="[requiredRule]"
            />
          </v-col>
          <v-col v-if="isAcademicFunded" cols="12" md="4">
            <v-text-field
              v-model.number="form.requestedAmount"
              :label="t('training.requestedAmount')"
              type="number"
              min="0"
              data-testid="training-form-amount"
            />
          </v-col>
        </v-row>
      </v-card>

      <!-- PDPA Restricted Bank Account Section (Category 1 only) -->
      <v-card v-if="isAcademicFunded" class="pa-6 mb-6">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-shield-lock-outline" color="primary" />
          {{ t('training.bankSection') }}
        </div>
        <v-alert type="info" variant="tonal" density="compact" class="mb-4">
          {{ t('training.bankPdpaNote') }}
        </v-alert>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.bankName" :label="t('training.bankName')" />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.bankAccountNumber" :label="t('training.bankAccountNumber')" />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.bankAccountHolderName" :label="t('training.bankAccountHolder')" />
          </v-col>
        </v-row>
      </v-card>

      <div class="d-flex justify-end ga-3">
        <v-btn variant="outlined" :disabled="submitting" to="/trainings/list">
          {{ t('common.cancel') }}
        </v-btn>
        <v-btn
          v-if="!isDirectRecordCategory"
          variant="tonal"
          color="secondary"
          :loading="submitting"
          @click="submitForm(false)"
        >
          {{ t('training.saveDraft') }}
        </v-btn>
        <v-btn
          color="primary"
          type="submit"
          :loading="submitting"
          prepend-icon="mdi-send-outline"
          data-testid="training-form-submit"
        >
          {{ isDirectRecordCategory ? t('training.recordDirect') : t('training.submitRequest') }}
        </v-btn>
      </div>
    </v-form>
  </div>
</template>
