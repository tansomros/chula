import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'

const cuPfisLightTheme = {
  dark: false,
  colors: {
    primary: '#C84B7A',
    'primary-darken-1': '#A13760',
    secondary: '#266A5A',
    'secondary-darken-1': '#1B4E42',
    accent: '#D97706',
    background: '#F6F5F8',
    'on-background': '#1F2430',
    surface: '#FFFFFF',
    'on-surface': '#1F2430',
    'surface-variant': '#F1EEF4',
    'on-surface-variant': '#1F2430',
    info: '#2563EB',
    success: '#15803D',
    warning: '#D97706',
    error: '#DC2626',
  },
}

export default createVuetify({
  theme: {
    defaultTheme: 'cuPfisLightTheme',
    themes: {
      cuPfisLightTheme,
    },
  },
  defaults: {
    VCard: {
      rounded: 'lg',
      elevation: 0,
      border: true,
    },
    VBtn: {
      rounded: 'lg',
    },
    VTextField: {
      variant: 'outlined',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VSelect: {
      variant: 'outlined',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VAutocomplete: {
      variant: 'outlined',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VTextarea: {
      variant: 'outlined',
      density: 'comfortable',
      hideDetails: 'auto',
    },
    VChip: {
      rounded: 'md',
    },
  },
})
