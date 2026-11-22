import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior: () => ({ top: 0 }),
})

// A18: First-login PDPA Consent gate checked against live CU-PFIS API (/api/pdpa-consents/me)
router.beforeEach(async (to) => {
  const authStore = useAuthStore()

  if (!authStore.pdpaLoaded) {
    await authStore.resolveLiveScopeIds()
    await authStore.fetchPdpaStatus()
  }

  if (!authStore.hasAcceptedPdpa && to.path !== '/pdpa-consent') {
    return {
      path: '/pdpa-consent',
      query: { redirect: to.fullPath },
    }
  }

  return true
})

export default router
