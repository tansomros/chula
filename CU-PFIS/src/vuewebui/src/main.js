import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { abilitiesPlugin } from '@casl/vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import i18n from './plugins/i18n'
import { ability } from './plugins/casl'
import { useAuthStore } from './stores/auth'
import './styles/main.css'

const app = createApp(App)
const pinia = createPinia()

app.use(pinia)

const authStore = useAuthStore(pinia)
authStore.syncAbility()

app.use(abilitiesPlugin, ability, { useGlobalProperties: true })
app.use(router)
app.use(vuetify)
app.use(i18n)

app.mount('#app')
