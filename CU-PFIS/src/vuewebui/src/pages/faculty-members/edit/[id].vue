<script setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter } from 'vue-router'
import { useFacultyMembersStore } from '@/stores/facultyMembers'
import FacultyForm from '@/components/FacultyForm.vue'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const facultyStore = useFacultyMembersStore()

const member = computed(() => facultyStore.getByExternalId(route.params.id))

function handleSubmit(payload) {
  facultyStore.updateMember(route.params.id, payload)
  router.push(`/faculty-members/view/${route.params.id}`)
}
</script>

<template>
  <div v-if="member">
    <div class="cu-page-header">
      <div class="d-flex align-center ga-3">
        <v-btn
          icon="mdi-arrow-left"
          variant="outlined"
          :to="`/faculty-members/view/${member.externalId}`"
        />
        <h1 class="text-h5 font-weight-bold">{{ t('faculty.editTitle') }}</h1>
      </div>
    </div>

    <FacultyForm
      :initial-value="member"
      is-edit
      @submit="handleSubmit"
      @cancel="router.push(`/faculty-members/view/${member.externalId}`)"
    />
  </div>

  <v-alert v-else type="warning" variant="tonal">
    {{ t('common.noData') }}
  </v-alert>
</template>
