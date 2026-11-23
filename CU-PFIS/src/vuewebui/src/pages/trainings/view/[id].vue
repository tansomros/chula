<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute } from 'vue-router'
import { useAbility } from '@casl/vue'
import { useAuthStore } from '@/stores/auth'
import { useTrainingsStore } from '@/stores/trainings'
import { useLocalizedField } from '@/composables/useLocalizedField'
import { usePdfPrint } from '@/composables/usePdfPrint'

const { t } = useI18n()
const route = useRoute()
const { can } = useAbility()
const authStore = useAuthStore()
const trainingsStore = useTrainingsStore()
const { pick } = useLocalizedField()
const { printCurrentView } = usePdfPrint()

const detail = computed(() => trainingsStore.currentDetail)
const actionLoading = ref(false)
const actionError = ref('')
const actionNote = ref('')
const revisionReason = ref('')
const approvedAmountInput = ref(0)
const showRevisionBox = ref(false)
const showApproveBox = ref(false)

const canVerify = computed(() => can('verify', 'training-requests'))
const canApprove = computed(() => can('approve', 'training-requests'))
const canEditRecord = computed(() => {
  if (!detail.value) return false
  return detail.value.status === 'Draft' || detail.value.status === 'RevisionRequested'
})

function formatMoney(val) {
  return Number(val || 0).toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

function statusColor(status) {
  const map = {
    Draft: 'default',
    Submitted: 'info',
    RevisionRequested: 'warning',
    VerifiedInProgress: 'secondary',
    Approved: 'success',
    Cancelled: 'error',
  }
  return map[status] || 'default'
}

async function loadDetail() {
  if (!route.params.id) return
  const d = await trainingsStore.fetchByExternalId(route.params.id)
  if (d) {
    approvedAmountInput.value = d.requestedAmount
  }
}

onMounted(loadDetail)
watch([() => route.params.id, () => authStore.personaKey], loadDetail)

async function runAction(fn) {
  actionLoading.value = true
  actionError.value = ''
  try {
    await fn()
    showRevisionBox.value = false
    showApproveBox.value = false
    actionNote.value = ''
    revisionReason.value = ''
  } catch (err) {
    actionError.value = err.message || t('common.apiError')
  } finally {
    actionLoading.value = false
  }
}
</script>

<template>
  <v-progress-linear v-if="trainingsStore.loading" indeterminate color="primary" class="mb-4" />

  <v-alert v-if="trainingsStore.error || actionError" type="error" variant="tonal" class="mb-4">
    {{ actionError || trainingsStore.error }}
  </v-alert>

  <div v-if="detail">
    <div class="cu-page-header no-print">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/trainings/list" />
        <div>
          <h1 class="text-h5 font-weight-bold">{{ t('training.viewTitle') }}</h1>
          <div class="text-caption cu-muted">
            {{ t('training.fiscalYear') }} {{ detail.fiscalYearBe }} · {{ t(`training.category.${detail.category}`) }}
          </div>
        </div>
      </div>

      <div class="d-flex flex-wrap ga-2">
        <v-btn
          variant="outlined"
          prepend-icon="mdi-printer-outline"
          data-testid="training-print-memo-btn"
          @click="printCurrentView"
        >
          {{ t('training.printMemo') }}
        </v-btn>
        <v-btn
          v-if="canEditRecord"
          color="primary"
          variant="outlined"
          prepend-icon="mdi-pencil-outline"
          :to="`/trainings/edit/${detail.externalId}`"
        >
          {{ t('common.edit') }}
        </v-btn>
      </div>
    </div>

    <!-- Staff / Owner Workflow Action Panel -->
    <v-card
      v-if="detail.status !== 'Approved' && detail.status !== 'Cancelled'"
      variant="outlined"
      class="pa-4 mb-5 no-print bg-surface-variant"
    >
      <div class="d-flex flex-wrap align-center justify-space-between ga-3">
        <div class="d-flex align-center ga-2">
          <v-chip :color="statusColor(detail.status)" variant="flat" size="small" data-testid="training-status-chip">
            {{ t(`training.status.${detail.status}`) }}
          </v-chip>
          <span v-if="detail.revisionNote" class="text-body-2 text-warning">
            — {{ detail.revisionNote }}
          </span>
        </div>

        <div class="d-flex flex-wrap ga-2">
          <v-btn
            v-if="detail.status === 'Draft' || detail.status === 'RevisionRequested'"
            color="primary"
            :loading="actionLoading"
            data-testid="training-submit-btn"
            @click="runAction(() => trainingsStore.submitTraining(detail.externalId))"
          >
            {{ t('training.submitRequest') }}
          </v-btn>

          <v-btn
            v-if="canVerify && detail.status === 'Submitted'"
            color="secondary"
            prepend-icon="mdi-check-decagram-outline"
            :loading="actionLoading"
            data-testid="training-verify-btn"
            @click="runAction(() => trainingsStore.verifyTraining(detail.externalId, actionNote || null))"
          >
            {{ t('training.verifyBtn') }}
          </v-btn>

          <v-btn
            v-if="canVerify && (detail.status === 'Submitted' || detail.status === 'VerifiedInProgress')"
            color="warning"
            variant="tonal"
            prepend-icon="mdi-undo-variant"
            data-testid="training-revision-toggle-btn"
            @click="showRevisionBox = !showRevisionBox"
          >
            {{ t('training.requestRevisionBtn') }}
          </v-btn>

          <v-btn
            v-if="canApprove && detail.status === 'VerifiedInProgress'"
            color="success"
            prepend-icon="mdi-seal"
            data-testid="training-approve-toggle-btn"
            @click="showApproveBox = !showApproveBox"
          >
            {{ t('training.approveBtn') }}
          </v-btn>

          <v-btn
            color="error"
            variant="text"
            :loading="actionLoading"
            data-testid="training-cancel-btn"
            @click="runAction(() => trainingsStore.cancelTraining(detail.externalId))"
          >
            {{ t('training.cancelBtn') }}
          </v-btn>
        </div>
      </div>

      <!-- Revision Reason Box -->
      <div v-if="showRevisionBox" class="mt-4 pt-4 border-t d-flex flex-wrap align-center ga-3">
        <v-text-field
          v-model="revisionReason"
          :label="t('training.revisionReasonLabel')"
          density="compact"
          hide-details
          class="flex-grow-1"
          data-testid="training-revision-reason-input"
        />
        <v-btn
          color="warning"
          :disabled="!revisionReason.trim()"
          :loading="actionLoading"
          data-testid="training-revision-confirm-btn"
          @click="runAction(() => trainingsStore.requestRevision(detail.externalId, revisionReason.trim()))"
        >
          {{ t('training.requestRevisionBtn') }}
        </v-btn>
      </div>

      <!-- Dean Approval Box -->
      <div v-if="showApproveBox" class="mt-4 pt-4 border-t">
        <v-row align="center" dense>
          <v-col v-if="detail.category === 'AcademicFunded'" cols="12" md="3">
            <v-text-field
              v-model.number="approvedAmountInput"
              :label="t('training.approvedAmount')"
              type="number"
              min="0"
              density="compact"
              hide-details
              data-testid="training-approve-amount-input"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="actionNote"
              :label="t('training.workflowNote')"
              density="compact"
              hide-details
            />
          </v-col>
          <v-col cols="12" md="3">
            <v-btn
              color="success"
              block
              :loading="actionLoading"
              data-testid="training-approve-confirm-btn"
              @click="
                runAction(() =>
                  trainingsStore.approveTraining(
                    detail.externalId,
                    detail.category === 'AcademicFunded' ? Number(approvedAmountInput) : 0,
                    actionNote || null,
                  ),
                )
              "
            >
              {{ t('training.approveBtn') }}
            </v-btn>
          </v-col>
        </v-row>
      </div>
    </v-card>

    <!-- Print-Ready Official Memorandum & Training Sheet -->
    <div class="cu-cv-sheet pa-6 pa-md-8 mb-6">
      <div class="d-flex flex-wrap justify-space-between align-start pb-4 mb-5 border-b">
        <div>
          <div class="text-caption font-weight-bold text-primary">{{ t('app.faculty') }} ({{ t('app.acronym') }})</div>
          <h2 class="text-h6 font-weight-bold mt-1">{{ t('training.memoHeader') }}</h2>
          <div class="text-body-2 cu-muted mt-1">{{ pick(detail.titleTh, detail.titleEn) }}</div>
        </div>
        <v-chip :color="statusColor(detail.status)" variant="tonal">
          {{ t(`training.status.${detail.status}`) }}
        </v-chip>
      </div>

      <v-row class="mb-5">
        <v-col cols="12" md="6">
          <div class="text-caption cu-muted">{{ t('faculty.fullName') }}</div>
          <div class="font-weight-bold mb-3">
            {{ pick(detail.facultyNameTh, detail.facultyNameEn) }} ({{ detail.personnelCode }})
          </div>
          <div class="text-caption cu-muted">{{ t('common.department') }}</div>
          <div class="mb-3">{{ pick(detail.departmentNameTh, detail.departmentNameEn) }}</div>
          <div class="text-caption cu-muted">{{ t('training.organizerTh') }}</div>
          <div>{{ pick(detail.organizerAndVenueTh, detail.organizerAndVenueEn) }}</div>
        </v-col>

        <v-col cols="12" md="6">
          <div class="text-caption cu-muted">{{ t('training.categoryLabel') }} / {{ t('training.workTypeLabel') }}</div>
          <div class="mb-3">
            {{ t(`training.category.${detail.category}`) }} · {{ t(`training.workType.${detail.workType}`) }}
          </div>
          <div class="text-caption cu-muted">{{ t('faculty.trainingDate') }}</div>
          <div class="mb-3">
            {{ detail.startDate }} – {{ detail.endDate }} ({{ t('training.fiscalYear') }} {{ detail.fiscalYearBe }})
          </div>
          <div class="text-caption cu-muted">{{ t('training.requestedAmount') }} / {{ t('training.approvedAmount') }}</div>
          <div class="text-h6 font-weight-bold text-primary">
            {{ formatMoney(detail.requestedAmount) }} THB
            <span v-if="detail.status === 'Approved'" class="text-body-2 text-success ml-2">
              ({{ t('training.approvedAmount') }}: {{ formatMoney(detail.approvedAmount) }} THB)
            </span>
          </div>
        </v-col>
      </v-row>

      <!-- Fiscal-Year Budget Snapshot -->
      <v-sheet v-if="detail.facultyFiscalYearBalance" border rounded="lg" class="pa-4 mb-5 bg-surface-variant">
        <div class="text-subtitle-2 font-weight-bold mb-2">
          {{ t('training.budgetSnapshot', { fy: detail.facultyFiscalYearBalance.fiscalYearBe }) }}
        </div>
        <v-row dense class="text-body-2">
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.annualQuota') }}</div>
            <strong>{{ formatMoney(detail.facultyFiscalYearBalance.annualQuota) }}</strong>
          </v-col>
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.consumed') }}</div>
            <strong>{{ formatMoney(detail.facultyFiscalYearBalance.consumedInYear) }}</strong>
          </v-col>
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.borrowedNext') }}</div>
            <strong class="text-warning">{{ formatMoney(detail.facultyFiscalYearBalance.borrowedFromNextYear) }}</strong>
          </v-col>
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.carryForward') }}</div>
            <strong class="text-error">{{ formatMoney(detail.facultyFiscalYearBalance.carryForwardDeductedInYear) }}</strong>
          </v-col>
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.remaining') }}</div>
            <strong class="text-success">{{ formatMoney(detail.facultyFiscalYearBalance.remainingInYear) }}</strong>
          </v-col>
          <v-col cols="6" md="2">
            <div class="text-caption cu-muted">{{ t('training.borrowableNext') }}</div>
            <strong class="text-primary">{{ formatMoney(detail.facultyFiscalYearBalance.borrowableFromNextYear) }}</strong>
          </v-col>
        </v-row>
      </v-sheet>

      <!-- PDPA-Gated Bank Account Details -->
      <div v-if="detail.category === 'AcademicFunded'" class="mb-5 no-print">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-shield-lock-outline" color="primary" />
          {{ t('training.bankSection') }}
        </div>
        <v-alert v-if="!detail.canViewBankAccount" type="warning" variant="tonal" density="compact">
          {{ t('training.bankPdpaNote') }}
        </v-alert>
        <v-sheet v-else border rounded="lg" class="pa-4">
          <v-row dense>
            <v-col cols="12" md="4">
              <div class="text-caption cu-muted">{{ t('training.bankName') }}</div>
              <strong>{{ detail.bankName || '-' }}</strong>
            </v-col>
            <v-col cols="12" md="4">
              <div class="text-caption cu-muted">{{ t('training.bankAccountNumber') }}</div>
              <strong>{{ detail.bankAccountNumber || '-' }}</strong>
            </v-col>
            <v-col cols="12" md="4">
              <div class="text-caption cu-muted">{{ t('training.bankAccountHolder') }}</div>
              <strong>{{ detail.bankAccountHolderName || '-' }}</strong>
            </v-col>
          </v-row>
        </v-sheet>
      </div>

      <!-- Ledger Entries -->
      <div v-if="detail.ledgerEntries?.length" class="mb-5">
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-book-account-outline" color="secondary" />
          {{ t('training.ledgerTitle') }}
        </div>
        <v-table density="compact" class="border rounded-lg">
          <thead>
            <tr>
              <th>{{ t('training.fiscalYear') }}</th>
              <th>Type</th>
              <th class="text-right">Amount (THB)</th>
              <th>Description</th>
              <th>Recorded By</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="entry in detail.ledgerEntries" :key="entry.externalId">
              <td>{{ entry.fiscalYearBe }}</td>
              <td>
                <v-chip size="x-small" color="primary" variant="tonal">
                  {{ t(`training.ledgerType.${entry.entryType}`) }}
                </v-chip>
              </td>
              <td class="text-right font-weight-bold">{{ formatMoney(entry.amount) }}</td>
              <td>{{ entry.description }}</td>
              <td class="text-caption">{{ entry.recordedBy }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>

      <!-- Approval History Trail -->
      <div>
        <div class="cu-section-title mb-2">
          <v-icon icon="mdi-timeline-check-outline" color="primary" />
          {{ t('training.approvalHistory') }}
        </div>
        <v-table density="compact" class="border rounded-lg">
          <thead>
            <tr>
              <th>Timestamp</th>
              <th>Transition</th>
              <th>Actor</th>
              <th>Note</th>
            </tr>
          </thead>
          <tbody>
            <tr v-if="!detail.approvalHistory?.length">
              <td colspan="4" class="text-center cu-muted py-4">{{ t('common.none') }}</td>
            </tr>
            <tr v-for="h in detail.approvalHistory" :key="h.externalId">
              <td class="text-caption text-no-wrap">{{ new Date(h.actionAt).toLocaleString() }}</td>
              <td>
                <span class="text-caption cu-muted">{{ t(`training.status.${h.fromStatus}`) }}</span>
                <v-icon icon="mdi-arrow-right" size="14" class="mx-1" />
                <v-chip size="x-small" :color="statusColor(h.toStatus)" variant="tonal">
                  {{ t(`training.status.${h.toStatus}`) }}
                </v-chip>
              </td>
              <td class="font-weight-medium">{{ h.actorLogin }}</td>
              <td>{{ h.note || '-' }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>
    </div>
  </div>

  <v-alert v-else-if="!trainingsStore.loading" type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
