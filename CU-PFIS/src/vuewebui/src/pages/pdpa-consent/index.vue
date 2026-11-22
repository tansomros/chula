<script setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { PDPA_POLICY_VERSION, useAuthStore } from '@/stores/auth'

const { t, locale } = useI18n()
const route = useRoute()
const router = useRouter()
const authStore = useAuthStore()

const agreed = ref(authStore.hasAcceptedPdpa)

function toggleLocale(nextLocale) {
  locale.value = nextLocale
  localStorage.setItem('cu-pfis-locale', nextLocale)
}

function handleAccept() {
  if (!agreed.value) return
  authStore.acceptPdpa()
  const redirectTarget = typeof route.query.redirect === 'string' ? route.query.redirect : '/'
  router.push(redirectTarget)
}
</script>

<template>
  <div
    class="min-h-screen d-flex align-center justify-center pa-4 pa-md-8"
    style="min-height: 100vh; background: linear-gradient(135deg, #fdf2f6 0%, #f2f8f6 100%)"
  >
    <v-card max-width="860" width="100%" class="pa-6 pa-md-8" elevation="2">
      <div class="d-flex flex-wrap align-center justify-space-between ga-3 mb-5">
        <div class="d-flex align-center ga-3">
          <v-avatar color="primary" size="48" rounded="lg">
            <v-icon icon="mdi-shield-account-outline" color="white" size="28" />
          </v-avatar>
          <div>
            <div class="text-caption font-weight-bold text-primary">
              {{ t('pdpa.subtitle') }}
            </div>
            <h1 class="text-h6 font-weight-bold">{{ t('pdpa.title') }}</h1>
          </div>
        </div>

        <v-btn-toggle
          :model-value="locale"
          mandatory
          density="compact"
          color="primary"
          variant="outlined"
          @update:model-value="toggleLocale"
        >
          <v-btn value="th" size="small">TH</v-btn>
          <v-btn value="en" size="small">EN</v-btn>
        </v-btn-toggle>
      </div>

      <v-alert
        v-if="!authStore.hasAcceptedPdpa"
        type="info"
        variant="tonal"
        class="mb-5"
        icon="mdi-information-outline"
      >
        {{ t('pdpa.firstLoginNotice') }}
      </v-alert>

      <v-alert
        v-else
        type="success"
        variant="tonal"
        class="mb-5"
        icon="mdi-check-circle-outline"
      >
        {{ t('pdpa.alreadyAccepted') }}
        <strong>{{ new Date(authStore.pdpaRecord.acceptedAt).toLocaleString() }}</strong>
        ({{ t('pdpa.versionLabel') }} {{ authStore.pdpaRecord.policyVersion }})
      </v-alert>

      <v-sheet
        border
        rounded="lg"
        class="pa-5 mb-5 bg-surface-variant"
        style="max-height: 360px; overflow-y: auto"
      >
        <div class="d-flex justify-space-between align-center mb-4">
          <v-chip size="small" color="secondary" variant="flat">
            {{ t('pdpa.versionLabel') }} {{ PDPA_POLICY_VERSION }}
          </v-chip>
        </div>

        <div class="mb-4">
          <h2 class="text-subtitle-1 font-weight-bold mb-1">{{ t('pdpa.sec1Title') }}</h2>
          <p class="text-body-2 mb-0">{{ t('pdpa.sec1Body') }}</p>
        </div>

        <div class="mb-4">
          <h2 class="text-subtitle-1 font-weight-bold mb-1">{{ t('pdpa.sec2Title') }}</h2>
          <p class="text-body-2 mb-0">{{ t('pdpa.sec2Body') }}</p>
        </div>

        <div class="mb-4">
          <h2 class="text-subtitle-1 font-weight-bold mb-1">{{ t('pdpa.sec3Title') }}</h2>
          <p class="text-body-2 mb-0">{{ t('pdpa.sec3Body') }}</p>
        </div>

        <div>
          <h2 class="text-subtitle-1 font-weight-bold mb-1">{{ t('pdpa.sec4Title') }}</h2>
          <p class="text-body-2 mb-0">{{ t('pdpa.sec4Body') }}</p>
        </div>
      </v-sheet>

      <v-checkbox
        v-model="agreed"
        color="primary"
        :label="t('pdpa.checkboxText')"
        data-testid="pdpa-consent-checkbox"
      />

      <div class="d-flex flex-wrap align-center justify-space-between ga-3 mt-2">
        <span class="text-caption cu-muted">{{ t('pdpa.declineNote') }}</span>

        <div class="d-flex ga-2">
          <v-btn v-if="authStore.hasAcceptedPdpa" variant="outlined" to="/">
            {{ t('common.back') }}
          </v-btn>
          <v-btn
            color="primary"
            size="large"
            :disabled="!agreed"
            prepend-icon="mdi-check-decagram-outline"
            data-testid="pdpa-accept-btn"
            @click="handleAccept"
          >
            {{ t('pdpa.acceptButton') }}
          </v-btn>
        </div>
      </div>
    </v-card>
  </div>
</template>
