import { createI18n } from 'vue-i18n'
import th from '@/locales/th.json'
import en from '@/locales/en.json'

const savedLocale = localStorage.getItem('cu-pfis-locale') || 'th'

const i18n = createI18n({
  legacy: false,
  locale: savedLocale,
  fallbackLocale: 'th',
  messages: {
    th,
    en,
  },
})

export default i18n
