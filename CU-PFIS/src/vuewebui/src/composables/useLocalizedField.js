import { useI18n } from 'vue-i18n'

export function useLocalizedField() {
  const { locale } = useI18n()

  function pick(thValue, enValue) {
    if (locale.value === 'en') {
      return enValue || thValue || '-'
    }
    return thValue || enValue || '-'
  }

  function formatFullName(person, includeTitle = false) {
    if (!person) return '-'
    if (locale.value === 'en') {
      const title = includeTitle && person.professionalTitleEn ? `${person.professionalTitleEn} ` : ''
      return `${title}${person.firstNameEn || person.firstNameTh} ${person.lastNameEn || person.lastNameTh}`.trim()
    }
    const title = includeTitle && person.professionalTitleTh ? `${person.professionalTitleTh}` : ''
    return `${title}${person.firstNameTh || person.firstNameEn} ${person.lastNameTh || person.lastNameEn}`.trim()
  }

  return {
    locale,
    pick,
    formatFullName,
  }
}
