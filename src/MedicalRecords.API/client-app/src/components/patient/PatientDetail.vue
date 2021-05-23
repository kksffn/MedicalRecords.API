<template>
  <div>
    <h2 v-if="patient.patientName != null" class="display-1 font-weight-bold mt-2 mb-5">
      {{ patient.patientName + ' ' + patient.patientSurname}} info
    </h2>
    <h2 v-if="patient.patientName == null" class="display-1 font-weight-bold mt-2 mb-5">
      Patient info
    </h2>
    <ul v-if="patient.patientName != null">
      <li style="text-align:left"><strong>Name:</strong> {{ patient.patientName }}</li>
      <li style="text-align:left"><strong>Surname:</strong> {{ patient.patientSurname }}</li>
      <li style="text-align:left"><strong>Date of birth:</strong> {{ patient.dateOfBirth }}</li>
      <li style="text-align:left"><strong>Phone nuber:</strong>{{ patient.phoneNumber }}</li>
      <li v-for="factor in patient.riskFactorResponses" :key="factor.id"><strong>Risk factor:</strong> {{ factor.factor }}</li>
      <li style="text-align:left">
        <v-btn variant="default">Details</v-btn>
      </li>
      <li style="text-align:left">
        <v-btn variant="success">Update</v-btn>
      </li>
      <li style="text-align:left">
        <v-btn variant="danger">Delete</v-btn>
      </li>
    </ul>
  </div>
</template>
<script>
import PatientService from '@/api-services/patient.service'
export default {
  name: 'PatientDetail',
  data () {
    return {
      patient: {}
    }
  },
  created () {
    if (this.$router.currentRoute.params.id) {
      PatientService.get(this.$router.currentRoute.params.id).then((response) => {
        this.patient = response.data
        console.log(this.patient)
      })
    }
  }

}
</script>
