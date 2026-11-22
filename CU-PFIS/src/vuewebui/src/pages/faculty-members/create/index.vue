<script setup>
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import FacultyForm from '@/components/FacultyForm.vue'

const { t } = useI18n()
const router = useRouter()
const facultyStore = useFacultyMembersStore()

function handleSubmit(payload) {
  const created = facultyStore.createMember(payload)
  router.push(`/faculty-members/view/${created.externalId}`)
}
</script>

<template>
  <div>
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn icon="mdi-arrow-left" variant="outlined" to="/faculty-members/list" />
        <h1 class="text-h5 font-weight-bold">{{ t('faculty.createTitle') }}</h1>
      </div>
    </div>

    <FacultyForm @submit="handleSubmit" @cancel="router.push('/faculty-members/list')" />
  </div>
</template>
