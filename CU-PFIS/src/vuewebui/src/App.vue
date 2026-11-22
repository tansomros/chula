<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import AppShell from '@/layouts/AppShell.vue'

const route = useRoute()
const isStandalonePage = computed(() => route.path === '/pdpa-consent')
</script>

<template>
  <v-app>
    <router-view v-if="isStandalonePage" />
    <AppShell v-else>
      <router-view />
    </AppShell>
  </v-app>
</template>
