<script setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useAbility } from '@casl/vue'
import { useAuthStore } from '@/stores/auth'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import { useStudentsStore } from '@/stores/students'
import { useReferenceDataStore } from '@/stores/referenceData'

const { t } = useI18n()
const { can } = useAbility()
const authStore = useAuthStore()
const facultyStore = useFacultyMembersStore()
const studentsStore = useStudentsStore()
const refStore = useReferenceDataStore()

const visibleFacultyCount = computed(() =>
  facultyStore.items.filter((f) => authStore.canAccessFaculty(f.externalId, f.departmentExternalId)).length,
)

const canViewStudents = computed(() => can('view', 'students-cv'))
</script>

<template>
  <div>
    <!-- Hero Banner -->
    <v-card class="pa-6 mb-6" style="background: linear-gradient(135deg, #fff5f8 0%, #f1f8f6 100%)">
      <div class="d-flex flex-wrap align-center justify-space-between ga-4">
        <div style="max-width: 780px">
          <v-chip color="primary" size="small" variant="flat" class="mb-3">
            {{ t('app.phaseBadge') }}
          </v-chip>
          <h1 class="text-h5 font-weight-bold mb-2">{{ t('home.welcome') }}</h1>
          <p class="text-body-1 cu-muted mb-0">{{ t('home.subtitle') }}</p>
        </div>
        <div class="d-flex flex-wrap ga-2">
          <v-btn
            color="primary"
            prepend-icon="mdi-account-tie-outline"
            to="/faculty-members/list"
            data-testid="home-open-faculty"
          >
            {{ t('nav.facultyCv') }}
          </v-btn>
          <v-btn
            v-if="canViewStudents"
            color="secondary"
            prepend-icon="mdi-school-outline"
            to="/students/list"
            data-testid="home-open-students"
          >
            {{ t('nav.studentCv') }}
          </v-btn>
        </div>
      </div>
    </v-card>

    <!-- KPI Row -->
    <v-row class="mb-6">
      <v-col cols="12" sm="6" md="3">
        <v-card class="pa-5">
          <div class="text-caption cu-muted">{{ t('home.kpiFaculty') }}</div>
          <div class="text-h4 font-weight-bold text-primary mt-1">{{ visibleFacultyCount }}</div>
        </v-card>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-card class="pa-5">
          <div class="text-caption cu-muted">{{ t('home.kpiStudents') }}</div>
          <div class="text-h4 font-weight-bold text-secondary mt-1">
            {{ canViewStudents ? studentsStore.items.length : '-' }}
          </div>
        </v-card>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-card class="pa-5">
          <div class="text-caption cu-muted">{{ t('home.kpiAcademicYears') }}</div>
          <div class="text-h4 font-weight-bold mt-1">{{ refStore.academicYears.length }}+</div>
        </v-card>
      </v-col>
      <v-col cols="12" sm="6" md="3">
        <v-card class="pa-5">
          <div class="text-caption cu-muted">{{ t('home.kpiPdpaStatus') }}</div>
          <div class="d-flex align-center ga-2 mt-2">
            <v-chip color="success" variant="tonal" size="small">
              <v-icon start icon="mdi-shield-check" size="16" />
              {{ t('app.pdpaAccepted') }}
            </v-chip>
          </div>
        </v-card>
      </v-col>
    </v-row>

    <!-- Phase 1 Live Module Cards -->
    <v-row class="mb-6">
      <v-col cols="12" md="4">
        <v-card class="pa-6 h-100 d-flex flex-column">
          <div class="d-flex align-center justify-space-between mb-3">
            <v-avatar color="primary" variant="tonal" size="44">
              <v-icon icon="mdi-account-tie-outline" />
            </v-avatar>
            <v-chip size="x-small" color="success" variant="tonal">{{ t('home.phase1Ready') }}</v-chip>
          </div>
          <h2 class="text-h6 font-weight-bold mb-2">{{ t('nav.facultyCv') }}</h2>
          <p class="text-body-2 cu-muted flex-grow-1">{{ t('home.facultyCardDesc') }}</p>
          <v-btn color="primary" variant="tonal" to="/faculty-members/list" block>
            {{ t('home.openModule') }}
          </v-btn>
        </v-card>
      </v-col>

      <v-col cols="12" md="4">
        <v-card class="pa-6 h-100 d-flex flex-column">
          <div class="d-flex align-center justify-space-between mb-3">
            <v-avatar color="secondary" variant="tonal" size="44">
              <v-icon icon="mdi-school-outline" />
            </v-avatar>
            <v-chip size="x-small" color="success" variant="tonal">{{ t('home.phase1Ready') }}</v-chip>
          </div>
          <h2 class="text-h6 font-weight-bold mb-2">{{ t('nav.studentCv') }}</h2>
          <p class="text-body-2 cu-muted flex-grow-1">{{ t('home.studentCardDesc') }}</p>
          <v-btn
            color="secondary"
            variant="tonal"
            to="/students/list"
            :disabled="!canViewStudents"
            block
          >
            {{ canViewStudents ? t('home.openModule') : t('common.ownCvOnlyNotice') }}
          </v-btn>
        </v-card>
      </v-col>

      <v-col cols="12" md="4">
        <v-card class="pa-6 h-100 d-flex flex-column">
          <div class="d-flex align-center justify-space-between mb-3">
            <v-avatar color="info" variant="tonal" size="44">
              <v-icon icon="mdi-shield-check-outline" />
            </v-avatar>
            <v-chip size="x-small" color="info" variant="tonal">A18</v-chip>
          </div>
          <h2 class="text-h6 font-weight-bold mb-2">{{ t('nav.pdpaConsent') }}</h2>
          <p class="text-body-2 cu-muted flex-grow-1">{{ t('home.pdpaCardDesc') }}</p>
          <v-btn color="info" variant="tonal" to="/pdpa-consent" block>
            {{ t('home.openModule') }}
          </v-btn>
        </v-card>
      </v-col>
    </v-row>

    <!-- Architecture Highlights -->
    <v-card class="pa-6">
      <div class="cu-section-title mb-4">
        <v-icon icon="mdi-check-decagram-outline" color="secondary" />
        {{ t('home.architectureHighlights') }}
      </div>
      <v-row>
        <v-col cols="12" md="6">
          <div class="d-flex align-start ga-3 mb-3">
            <v-icon icon="mdi-translate" color="primary" class="mt-1" />
            <span class="text-body-2">{{ t('home.highlightBilingual') }}</span>
          </div>
          <div class="d-flex align-start ga-3">
            <v-icon icon="mdi-account-lock-outline" color="primary" class="mt-1" />
            <span class="text-body-2">{{ t('home.highlightDeptScope') }}</span>
          </div>
        </v-col>
        <v-col cols="12" md="6">
          <div class="d-flex align-start ga-3 mb-3">
            <v-icon icon="mdi-sync-alert" color="secondary" class="mt-1" />
            <span class="text-body-2">{{ t('home.highlightNonDestructive') }}</span>
          </div>
          <div class="d-flex align-start ga-3">
            <v-icon icon="mdi-printer-outline" color="secondary" class="mt-1" />
            <span class="text-body-2">{{ t('home.highlightDedicatedRoutes') }}</span>
          </div>
        </v-col>
      </v-row>
    </v-card>
  </div>
</template>
