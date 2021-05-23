<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="6">
        <v-card elevation="3">
          <v-system-bar>
          </v-system-bar>
          <v-col class="mb-4">
            <PatientList />
          </v-col>
        </v-card>
      </v-col>
      <v-col cols="6">
        <v-card elevation="10">
          <v-system-bar>
            <v-spacer></v-spacer>
            <v-icon>mdi-close</v-icon>
          </v-system-bar>
          <PatientDetail />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import PatientList from '@/components/patient/PatientList.vue'
import PatientDetail from '@/components/patient/PatientDetail.vue'
export default {
  name: 'Content',
  components: {
    PatientList,
    PatientDetail
  }
}
</script>
