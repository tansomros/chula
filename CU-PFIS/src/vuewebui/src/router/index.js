import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior: () => ({ top: 0 }),
})

// A18: First-login PDPA Consent gate before accessing any CU-PFIS module
router.beforeEach((to) => {
  const authStore = useAuthStore()

  if (!authStore.hasAcceptedPdpa && to.path !== '/pdpa-consent') {
    return {
      path: '/pdpa-consent',
      query: { redirect: to.fullPath },
    }
  }

  return true
})

export default router
