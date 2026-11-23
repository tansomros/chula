<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAbility } from '@casl/vue'
import { useAuthStore } from '@/stores/auth'
import { useTrainingsStore } from '@/stores/trainings'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const { can } = useAbility()
const authStore = useAuthStore()
const trainingsStore = useTrainingsStore()
const { pick } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const selectedFiscalYear = ref(2570)
const savingPolicy = ref(false)
const savingAllocation = ref(false)
const editingFacultyId = ref(null)
const snackbar = ref(false)
const snackbarText = ref('')
const snackbarColor = ref('success')

const policyForm = reactive({
  annualQuotaAmount: 10000,
  allowFutureQuotaBorrowing: true,
  maxBorrowAmount: 10000,
  resetAllCustomAllocations: false,
})

const allocationForm = reactive({
  annualQuotaAmount: 10000,
  maxBorrowAmount: '',
  note: '',
})

const canManagePolicy = computed(() => can('manage', 'training-budget-policies'))

const fiscalYearOptions = computed(() => {
  const fromPolicies = trainingsStore.policies.map((p) => p.fiscalYearBe)
  const years = Array.from(new Set([2568, 2569, 2570, 2571, ...fromPolicies])).sort((a, b) => b - a)
  return years.map((fy) => ({ value: fy, title: `${t('training.fiscalYear')} ${fy}` }))
})

const budgetItems = computed(() => trainingsStore.budgetSummary?.items || [])

const totals = computed(() =>
  budgetItems.value.reduce(
    (acc, row) => ({
      quota: acc.quota + Number(row.annualQuota || 0),
      consumed: acc.consumed + Number(row.consumedInYear || 0),
      borrowed: acc.borrowed + Number(row.borrowedFromNextYear || 0),
      remaining: acc.remaining + Number(row.remainingInYear || 0),
    }),
    { quota: 0, consumed: 0, borrowed: 0, remaining: 0 },
  ),
)

function formatMoney(val) {
  return Number(val || 0).toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

function syncPolicyFormFromSummary() {
  const s = trainingsStore.budgetSummary
  if (!s) return
  policyForm.annualQuotaAmount = s.policyAnnualQuota
  policyForm.allowFutureQuotaBorrowing = s.allowFutureQuotaBorrowing
  policyForm.maxBorrowAmount = s.policyMaxBorrowAmount
}

async function loadBudgets() {
  editingFacultyId.value = null
  await Promise.all([
    trainingsStore.fetchPolicies(),
    trainingsStore.fetchFacultyBudgets(selectedFiscalYear.value),
  ])
  syncPolicyFormFromSummary()
}

onMounted(loadBudgets)
watch([() => authStore.personaKey, selectedFiscalYear], loadBudgets)

async function handleSavePolicy() {
  savingPolicy.value = true
  try {
    await trainingsStore.upsertPolicy(selectedFiscalYear.value, {
      annualQuotaAmount: Number(policyForm.annualQuotaAmount) || 0,
      allowFutureQuotaBorrowing: Boolean(policyForm.allowFutureQuotaBorrowing),
      maxBorrowAmount: Number(policyForm.maxBorrowAmount) || 0,
      resetAllCustomAllocations: Boolean(policyForm.resetAllCustomAllocations),
    })
    policyForm.resetAllCustomAllocations = false
    editingFacultyId.value = null
    await trainingsStore.fetchFacultyBudgets(selectedFiscalYear.value)
    syncPolicyFormFromSummary()
    snackbarColor.value = 'success'
    snackbarText.value = t('training.policySaved', { fy: selectedFiscalYear.value })
    snackbar.value = true
  } catch (err) {
    snackbarColor.value = 'error'
    snackbarText.value = err.message || t('common.apiError')
    snackbar.value = true
  } finally {
    savingPolicy.value = false
  }
}

function startEditAllocation(row) {
  if (editingFacultyId.value === row.facultyMemberExternalId) {
    editingFacultyId.value = null
    return
  }
  editingFacultyId.value = row.facultyMemberExternalId
  allocationForm.annualQuotaAmount = Number(row.annualQuota ?? policyForm.annualQuotaAmount)
  allocationForm.maxBorrowAmount =
    row.customMaxBorrowAmount !== null && row.customMaxBorrowAmount !== undefined
      ? String(row.customMaxBorrowAmount)
      : ''
  allocationForm.note = row.allocationNote || ''
}

function cancelEditAllocation() {
  editingFacultyId.value = null
}

async function handleSaveAllocation(row) {
  savingAllocation.value = true
  try {
    const rawBorrow = allocationForm.maxBorrowAmount
    const maxBorrowAmount =
      rawBorrow === '' || rawBorrow === null || rawBorrow === undefined
        ? null
        : Number(rawBorrow)

    await trainingsStore.upsertFacultyAllocation(
      selectedFiscalYear.value,
      row.facultyMemberExternalId,
      {
        annualQuotaAmount: Number(allocationForm.annualQuotaAmount) || 0,
        maxBorrowAmount,
        note: allocationForm.note?.trim() || null,
      },
    )
    editingFacultyId.value = null
    await trainingsStore.fetchFacultyBudgets(selectedFiscalYear.value)
    snackbarColor.value = 'success'
    snackbarText.value = t('training.allocationSaved')
    snackbar.value = true
  } catch (err) {
    snackbarColor.value = 'error'
    snackbarText.value = err.message || t('common.apiError')
    snackbar.value = true
  } finally {
    savingAllocation.value = false
  }
}

async function handleRevertAllocation(row) {
  savingAllocation.value = true
  try {
    await trainingsStore.deleteFacultyAllocation(
      selectedFiscalYear.value,
      row.facultyMemberExternalId,
    )
    editingFacultyId.value = null
    await trainingsStore.fetchFacultyBudgets(selectedFiscalYear.value)
    snackbarColor.value = 'success'
    snackbarText.value = t('training.allocationReverted')
    snackbar.value = true
  } catch (err) {
    snackbarColor.value = 'error'
    snackbarText.value = err.message || t('common.apiError')
    snackbar.value = true
  } finally {
    savingAllocation.value = false
  }
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div>
        <h1 class="text-h5 font-weight-bold">{{ t('training.tabFacultyBudgets') }}</h1>
        <p class="text-body-2 cu-muted mb-0">
          {{ t('training.fiscalYearRange') }}
          <template v-if="trainingsStore.budgetSummary">
            ({{ trainingsStore.budgetSummary.fiscalYearStartDate }} – {{ trainingsStore.budgetSummary.fiscalYearEndDate }})
          </template>
        </p>
      </div>

      <div class="d-flex flex-wrap ga-2 no-print">
        <v-btn variant="outlined" prepend-icon="mdi-printer-outline" @click="printCurrentView">
          {{ t('common.exportPdf') }}
        </v-btn>
        <v-btn color="primary" prepend-icon="mdi-plus" to="/trainings/create">
          {{ t('common.create') }}
        </v-btn>
      </div>
    </div>

    <v-alert
      v-if="authStore.isStaff"
      type="info"
      variant="tonal"
      density="compact"
      class="mb-4"
      icon="mdi-office-building-outline"
    >
      {{ t('common.departmentScopeNotice') }} —
      <strong>{{ pick(authStore.currentUser.departmentNameTh, authStore.currentUser.departmentNameEn) }}</strong>
    </v-alert>

    <v-alert v-if="trainingsStore.error" type="error" variant="tonal" density="compact" class="mb-4">
      {{ trainingsStore.error }}
    </v-alert>

    <!-- Fiscal Year Filter & KPI Summary -->
    <v-row class="mb-5">
      <v-col cols="12" md="3">
        <v-card class="pa-4 h-100 d-flex align-center">
          <v-select
            v-model="selectedFiscalYear"
            :items="fiscalYearOptions"
            :label="t('training.fiscalYear')"
            hide-details
            class="w-100"
            data-testid="budgets-fy-select"
          />
        </v-card>
      </v-col>
      <v-col cols="12" sm="4" md="3">
        <v-card class="pa-4">
          <div class="text-caption cu-muted">{{ t('training.annualQuota') }}</div>
          <div class="text-h6 font-weight-bold text-primary mt-1">{{ formatMoney(totals.quota) }}</div>
        </v-card>
      </v-col>
      <v-col cols="12" sm="4" md="3">
        <v-card class="pa-4">
          <div class="text-caption cu-muted">{{ t('training.consumed') }}</div>
          <div class="text-h6 font-weight-bold mt-1">{{ formatMoney(totals.consumed) }}</div>
        </v-card>
      </v-col>
      <v-col cols="12" sm="4" md="3">
        <v-card class="pa-4">
          <div class="text-caption cu-muted">{{ t('training.remaining') }}</div>
          <div class="text-h6 font-weight-bold text-success mt-1">{{ formatMoney(totals.remaining) }}</div>
        </v-card>
      </v-col>
    </v-row>

    <!-- Admin Policy Configuration Bar -->
    <v-card v-if="canManagePolicy" variant="outlined" class="pa-4 mb-5 bg-surface-variant no-print">
      <div class="text-subtitle-2 font-weight-bold mb-3">
        <v-icon icon="mdi-tune" size="18" class="mr-1" />
        {{ t('training.policyTitle') }} — {{ selectedFiscalYear }}
      </div>
      <v-row align="center" dense>
        <v-col cols="12" md="3">
          <v-text-field
            v-model.number="policyForm.annualQuotaAmount"
            :label="t('training.annualQuota')"
            type="number"
            min="0"
            density="compact"
            hide-details
            data-testid="policy-quota-input"
          />
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field
            v-model.number="policyForm.maxBorrowAmount"
            :label="t('training.maxBorrowAmount')"
            type="number"
            min="0"
            :disabled="!policyForm.allowFutureQuotaBorrowing"
            density="compact"
            hide-details
          />
        </v-col>
        <v-col cols="12" md="4">
          <v-switch
            v-model="policyForm.allowFutureQuotaBorrowing"
            :label="t('training.allowBorrow')"
            color="primary"
            density="compact"
            hide-details
          />
        </v-col>
        <v-col cols="12" md="2">
          <v-btn
            color="primary"
            block
            :loading="savingPolicy"
            data-testid="policy-save-btn"
            @click="handleSavePolicy"
          >
            {{ t('training.savePolicy') }}
          </v-btn>
        </v-col>
      </v-row>
      <div class="mt-2">
        <v-checkbox
          v-model="policyForm.resetAllCustomAllocations"
          :label="t('training.resetAllCustomAllocations')"
          color="warning"
          density="compact"
          hide-details
          data-testid="policy-reset-custom-checkbox"
        />
      </div>
    </v-card>

    <!-- Per-Faculty Fiscal-Year Budget Table -->
    <v-card>
      <v-progress-linear v-if="trainingsStore.loading" indeterminate color="primary" />
      <v-table density="comfortable" hover data-testid="faculty-budgets-table">
        <thead>
          <tr>
            <th>{{ t('faculty.fullName') }}</th>
            <th>{{ t('common.department') }}</th>
            <th class="text-right">{{ t('training.annualQuota') }}</th>
            <th class="text-right">{{ t('training.consumed') }}</th>
            <th class="text-right">{{ t('training.borrowedNext') }}</th>
            <th class="text-right">{{ t('training.carryForward') }}</th>
            <th class="text-right">{{ t('training.remaining') }}</th>
            <th class="text-right">{{ t('training.borrowableNext') }}</th>
            <th class="text-center">{{ t('training.approvedCount') }}</th>
            <th v-if="canManagePolicy" class="text-center no-print">{{ t('common.actions') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="!trainingsStore.loading && budgetItems.length === 0">
            <td :colspan="canManagePolicy ? 10 : 9" class="text-center py-8 cu-muted">{{ t('common.noData') }}</td>
          </tr>
          <template v-for="row in budgetItems" :key="row.facultyMemberExternalId">
            <tr>
              <td>
                <router-link
                  :to="`/faculty-members/view/${row.facultyMemberExternalId}`"
                  class="font-weight-bold text-primary text-decoration-none"
                >
                  {{ pick(row.facultyNameTh, row.facultyNameEn) }}
                </router-link>
                <div class="text-caption cu-muted">{{ row.personnelCode }}</div>
              </td>
              <td>{{ pick(row.departmentNameTh, row.departmentNameEn) }}</td>
              <td class="text-right">
                <div>{{ formatMoney(row.annualQuota) }}</div>
                <div v-if="row.hasCustomAllocation" class="mt-1">
                  <v-chip
                    size="x-small"
                    color="warning"
                    variant="tonal"
                    data-testid="custom-allocation-chip"
                  >
                    {{ t('training.customAllocationBadge') }}
                  </v-chip>
                </div>
                <div
                  v-if="row.allocationNote"
                  class="text-caption cu-muted text-truncate"
                  style="max-width: 180px; margin-left: auto;"
                  :title="row.allocationNote"
                >
                  {{ row.allocationNote }}
                </div>
              </td>
              <td class="text-right">{{ formatMoney(row.consumedInYear) }}</td>
              <td class="text-right text-warning font-weight-medium">
                {{ row.borrowedFromNextYear > 0 ? formatMoney(row.borrowedFromNextYear) : '-' }}
              </td>
              <td class="text-right text-error">
                {{ row.carryForwardDeductedInYear > 0 ? `-${formatMoney(row.carryForwardDeductedInYear)}` : '-' }}
              </td>
              <td class="text-right font-weight-bold text-success">
                {{ formatMoney(row.remainingInYear) }}
              </td>
              <td class="text-right cu-muted">{{ formatMoney(row.borrowableFromNextYear) }}</td>
              <td class="text-center">
                <v-chip size="x-small" color="secondary" variant="tonal">
                  {{ row.approvedTrainingsCount }}
                </v-chip>
              </td>
              <td v-if="canManagePolicy" class="text-center no-print">
                <v-btn
                  size="small"
                  variant="text"
                  color="primary"
                  icon="mdi-pencil-outline"
                  :title="t('training.editIndividualQuota')"
                  data-testid="edit-allocation-btn"
                  @click="startEditAllocation(row)"
                />
              </td>
            </tr>
            <tr
              v-if="canManagePolicy && editingFacultyId === row.facultyMemberExternalId"
              class="bg-surface-variant no-print"
              data-testid="allocation-inline-editor"
            >
              <td colspan="10" class="pa-4">
                <div class="text-subtitle-2 font-weight-bold mb-3">
                  <v-icon icon="mdi-account-cash-outline" size="18" class="mr-1" />
                  {{ t('training.editIndividualQuota') }} — {{ pick(row.facultyNameTh, row.facultyNameEn) }} ({{ selectedFiscalYear }})
                </div>
                <v-row align="center" dense>
                  <v-col cols="12" md="3">
                    <v-text-field
                      v-model.number="allocationForm.annualQuotaAmount"
                      :label="t('training.annualQuota')"
                      type="number"
                      min="0"
                      density="compact"
                      hide-details
                      data-testid="allocation-quota-input"
                    />
                  </v-col>
                  <v-col cols="12" md="3">
                    <v-text-field
                      v-model="allocationForm.maxBorrowAmount"
                      :label="t('training.maxBorrowAmount')"
                      :placeholder="t('training.customMaxBorrowHint')"
                      type="number"
                      min="0"
                      density="compact"
                      clearable
                      hide-details
                      data-testid="allocation-max-borrow-input"
                    />
                  </v-col>
                  <v-col cols="12" md="6">
                    <v-text-field
                      v-model="allocationForm.note"
                      :label="t('training.allocationNote')"
                      density="compact"
                      hide-details
                      data-testid="allocation-note-input"
                    />
                  </v-col>
                </v-row>
                <div class="d-flex flex-wrap ga-2 mt-3 justify-end">
                  <v-btn
                    v-if="row.hasCustomAllocation"
                    variant="outlined"
                    color="warning"
                    prepend-icon="mdi-restore"
                    :loading="savingAllocation"
                    data-testid="revert-allocation-btn"
                    @click="handleRevertAllocation(row)"
                  >
                    {{ t('training.revertToGlobal') }}
                  </v-btn>
                  <v-btn
                    variant="text"
                    :disabled="savingAllocation"
                    @click="cancelEditAllocation"
                  >
                    {{ t('common.cancel') }}
                  </v-btn>
                  <v-btn
                    color="primary"
                    prepend-icon="mdi-content-save-outline"
                    :loading="savingAllocation"
                    data-testid="save-allocation-btn"
                    @click="handleSaveAllocation(row)"
                  >
                    {{ t('common.save') }}
                  </v-btn>
                </div>
              </td>
            </tr>
          </template>
        </tbody>
      </v-table>
    </v-card>

    <v-snackbar v-model="snackbar" :color="snackbarColor" timeout="4000">
      {{ snackbarText }}
    </v-snackbar>
  </div>
</template>
