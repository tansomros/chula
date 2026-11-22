<script setup>
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useAbility } from '@casl/vue'
import { DEMO_PERSONAS, useAuthStore } from '@/stores/auth'
import { useReferenceDataStore } from '@/stores/referenceData'
import { useLocalizedField } from '@/composables/useLocalizedField'

const { t, locale } = useI18n()
const route = useRoute()
const router = useRouter()
const { can } = useAbility()
const authStore = useAuthStore()
const refStore = useReferenceDataStore()
const { pick } = useLocalizedField()

const drawer = ref(true)
const switchingPersona = ref(false)

const personaOptions = computed(() =>
  Object.values(DEMO_PERSONAS).map((persona) => ({
    value: persona.id,
    title: pick(persona.displayNameTh, persona.displayNameEn),
    role: persona.role,
  })),
)

const canViewStudents = computed(() => can('view', 'students-cv'))
const globalApiError = computed(() => authStore.apiError || refStore.error)

function toggleLocale(nextLocale) {
  locale.value = nextLocale
  localStorage.setItem('cu-pfis-locale', nextLocale)
  document.documentElement.lang = nextLocale
}

async function handlePersonaChange(nextPersona) {
  switchingPersona.value = true
  try {
    await authStore.setPersona(nextPersona)
    if (!authStore.hasAcceptedPdpa && route.path !== '/pdpa-consent') {
      await router.push({ path: '/pdpa-consent', query: { redirect: route.fullPath } })
      return
    }
    if (nextPersona === 'faculty' && route.path.startsWith('/students')) {
      await router.push('/faculty-members/list')
    }
  } finally {
    switchingPersona.value = false
  }
}

function handleResetPdpa() {
  authStore.resetPdpaLocalPreview()
  router.push('/pdpa-consent')
}

async function retryApiConnection() {
  await authStore.resolveLiveScopeIds()
  await Promise.all([authStore.fetchPdpaStatus(), refStore.ensureLoaded(true)])
}
</script>

<template>
  <v-navigation-drawer v-model="drawer" width="276" color="surface" border="e">
    <div class="pa-4 border-b">
      <div class="d-flex align-center ga-3">
        <v-avatar color="primary" size="42" rounded="lg">
          <v-icon icon="mdi-mortar-board" color="white" />
        </v-avatar>
        <div class="overflow-hidden">
          <div class="text-subtitle-1 font-weight-bold text-primary line-height-tight">
            {{ t('app.acronym') }}
          </div>
          <div class="text-caption cu-muted text-truncate">
            {{ t('app.faculty') }}
          </div>
        </div>
      </div>
      <v-chip size="x-small" color="secondary" variant="tonal" class="mt-3">
        <v-icon start icon="mdi-api" size="12" />
        {{ t('app.phaseBadge') }}
      </v-chip>
    </div>

    <v-list density="comfortable" nav class="pa-3">
      <v-list-item
        to="/"
        prepend-icon="mdi-view-dashboard-outline"
        :title="t('nav.overview')"
        color="primary"
        rounded="lg"
        data-testid="nav-overview"
      />

      <v-list-subheader class="mt-3 text-uppercase font-weight-bold">
        {{ t('nav.phase1Group') }}
      </v-list-subheader>

      <v-list-item
        to="/faculty-members/list"
        prepend-icon="mdi-account-tie-outline"
        :title="t('nav.facultyCv')"
        color="primary"
        rounded="lg"
        data-testid="nav-faculty-cv"
      />

      <v-list-item
        v-if="canViewStudents"
        to="/students/list"
        prepend-icon="mdi-school-outline"
        :title="t('nav.studentCv')"
        color="primary"
        rounded="lg"
        data-testid="nav-student-cv"
      />

      <v-list-item
        to="/pdpa-consent"
        prepend-icon="mdi-shield-check-outline"
        :title="t('nav.pdpaConsent')"
        color="secondary"
        rounded="lg"
        data-testid="nav-pdpa-consent"
      />

      <v-list-subheader class="mt-4 text-uppercase font-weight-bold">
        {{ t('nav.upcomingGroup') }}
      </v-list-subheader>

      <v-list-item
        disabled
        prepend-icon="mdi-cash-multiple"
        :title="t('nav.training')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P2</v-chip>
        </template>
      </v-list-item>

      <v-list-item
        disabled
        prepend-icon="mdi-trophy-outline"
        :title="t('nav.achievements')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P3</v-chip>
        </template>
      </v-list-item>

      <v-list-item
        disabled
        prepend-icon="mdi-briefcase-check-outline"
        :title="t('nav.employment')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P3</v-chip>
        </template>
      </v-list-item>

      <v-list-item
        disabled
        prepend-icon="mdi-certificate-outline"
        :title="t('nav.license')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P3</v-chip>
        </template>
      </v-list-item>

      <v-list-item
        disabled
        prepend-icon="mdi-book-open-page-variant-outline"
        :title="t('nav.seniorProjects')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P4</v-chip>
        </template>
      </v-list-item>

      <v-list-item
        disabled
        prepend-icon="mdi-chart-box-outline"
        :title="t('nav.reports')"
        rounded="lg"
      >
        <template #append>
          <v-chip size="x-small" variant="tonal">P5</v-chip>
        </template>
      </v-list-item>
    </v-list>

    <template #append>
      <div class="pa-3 border-t bg-surface-variant">
        <div class="d-flex align-center justify-space-between mb-2">
          <v-chip
            size="x-small"
            :color="authStore.hasAcceptedPdpa ? 'success' : 'warning'"
            variant="flat"
          >
            <v-icon
              start
              :icon="authStore.hasAcceptedPdpa ? 'mdi-check-circle' : 'mdi-alert-circle-outline'"
              size="12"
            />
            {{ authStore.hasAcceptedPdpa ? t('app.pdpaAccepted') : t('app.pdpaPending') }}
          </v-chip>
          <v-btn
            size="x-small"
            variant="text"
            color="primary"
            data-testid="reset-pdpa-btn"
            @click="handleResetPdpa"
          >
            {{ t('app.resetPdpa') }}
          </v-btn>
        </div>
        <div class="text-caption font-weight-medium text-truncate">
          {{ pick(authStore.currentUser.displayNameTh, authStore.currentUser.displayNameEn) }}
        </div>
        <div class="text-caption cu-muted text-truncate">
          {{ pick(authStore.currentUser.departmentNameTh, authStore.currentUser.departmentNameEn) }}
        </div>
      </div>
    </template>
  </v-navigation-drawer>

  <v-app-bar flat border="b" color="surface" height="64">
    <v-app-bar-nav-icon @click="drawer = !drawer" />

    <div class="d-none d-md-flex flex-column ml-2">
      <span class="text-subtitle-2 font-weight-bold">{{ t('app.fullName') }}</span>
    </div>

    <v-spacer />

    <!-- Role preview switcher mapped to DevelopmentHeaderAuthMiddleware (X-Dev-*) -->
    <div class="mr-3" style="width: 280px">
      <v-select
        :model-value="authStore.personaKey"
        :items="personaOptions"
        :label="t('app.demoPersona')"
        :loading="switchingPersona"
        density="compact"
        hide-details
        prepend-inner-icon="mdi-account-switch-outline"
        data-testid="persona-switcher"
        @update:model-value="handlePersonaChange"
      />
    </div>

    <!-- TH / EN Bilingual Switcher (A15) -->
    <v-btn-toggle
      :model-value="locale"
      mandatory
      density="compact"
      color="primary"
      variant="outlined"
      class="mr-4"
      data-testid="locale-switcher"
      @update:model-value="toggleLocale"
    >
      <v-btn value="th" size="small" data-testid="locale-th">TH</v-btn>
      <v-btn value="en" size="small" data-testid="locale-en">EN</v-btn>
    </v-btn-toggle>
  </v-app-bar>

  <v-main class="bg-background">
    <v-container fluid class="pa-6" style="max-width: 1400px">
      <v-alert
        v-if="globalApiError"
        type="error"
        variant="tonal"
        class="mb-4 no-print"
        icon="mdi-lan-disconnect"
      >
        <div class="d-flex flex-wrap align-center justify-space-between ga-2">
          <div>
            <div class="font-weight-bold">{{ t('common.apiError') }}</div>
            <div class="text-caption">{{ globalApiError }}</div>
          </div>
          <v-btn size="small" color="error" variant="outlined" @click="retryApiConnection">
            {{ t('common.retry') }}
          </v-btn>
        </div>
      </v-alert>

      <slot />
    </v-container>
  </v-main>
</template>
