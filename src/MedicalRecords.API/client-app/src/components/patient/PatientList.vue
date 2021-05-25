<template>
  <div>
    <h1 class="display-2 font-weight-bold mb-2">
      List of all patients
    </h1>

    <!-- ################ CREATE NEW PATIENT ############################# -->
    <v-card class="d-flex flex-row-reverse"
            flat
            tile>
      <v-btn small color="primary"
             dark
             class="mr-2 mt-2 mb-2"
             @click="createPatient">
        Create new patient
      </v-btn>
    </v-card>
    <!-- ################ TABLE WITH ALL PATIENTS ############################# -->
    <v-card class="d-flex" pl-20 ml-10 mr-10 flat tile>
      <v-text-field label="Search by name or surname" prepend-icon="mdi-magnify"
                    @keyup="searchBy()" v-model="searchByString"> {{ searchByString }}
      </v-text-field>
    </v-card>
    <v-simple-table class="elevation-1">
      <template v-slot:default>
        <thead>
          <tr>
            <th class="text-left" cols="12" sm="6" md="4">
              <v-btn small text color="black" class="pl-0 ml-0"
                     @click="sortBy('patientName')" slot="activator">
                Name
                <v-icon v-if="ascending == 1 && sortByString == 'patientName'"
                        small
                        class="mr-2">
                  mdi-arrow-up
                </v-icon>
                <v-icon v-if="ascending == 2 && sortByString == 'patientName'"
                        small
                        class="mr-2">
                  mdi-arrow-down
                </v-icon>
              </v-btn>
            </th>
            <th class="text-left" cols="12" sm="6" md="4">
              <v-btn small text color="black" class="pl-0 ml-0"
                     @click="sortBy('patientSurname')" slot="activator">
                Surname
                <v-icon v-if="ascending == 1 && sortByString == 'patientSurname'"
                        small
                        class="mr-2">
                  mdi-arrow-up
                </v-icon>
                <v-icon v-if="ascending == 2 && sortByString == 'patientSurname'"
                        small
                        class="mr-2">
                  mdi-arrow-down
                </v-icon>
              </v-btn>
            </th>
            <th class="text-left" cols="12" sm="6" md="4">
              <v-btn small text color="black" class="pl-0 ml-0"
                     @click="sortBy('dateOfBirth')" slot="activator">
                Date of birth
                <v-icon v-if="ascending == 1 && sortByString == 'dateOfBirth'"
                        small
                        class="mr-2">
                  mdi-arrow-up
                </v-icon>
                <v-icon v-if="ascending == 2 && sortByString == 'dateOfBirth'"
                        small
                        class="mr-2">
                  mdi-arrow-down
                </v-icon>
              </v-btn>
            </th>
            <th class="text-center" cols="12" sm="6" md="4">
              <v-btn small text color="black" class="pl-0 ml-0" disabled>
                Action
              </v-btn>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="patient in patients.data"
              :key="patient.id">
            <td class="text-left" cols="12" sm="6" md="4">{{ patient.patientName }}</td>
            <td class="text-left" cols="12" sm="6" md="4">{{ patient.patientSurname }}</td>
            <td class="text-left" cols="12" sm="6" md="4">{{ patient.dateOfBirth }}</td>
            <td class="text-left" cols="12" sm="6" md="4">
              <v-icon large
                      class="mr-2"
                      @click="showDetail(patient)">
                mdi-information-outline
              </v-icon>
              <v-icon small
                      class="mr-2"
                      @click="editItem(patient)">
                mdi-pencil
              </v-icon>
              <v-icon small
                      @click="deleteItem(item)">
                mdi-delete
              </v-icon>
            </td>
          </tr>
        </tbody>
      </template>
    </v-simple-table>

    <!-- ################ PAGINATION ############################# -->
    <v-container>
      <v-row justify="center">
        <v-col cols="12">
          <v-container class="max-width">
            <v-pagination small v-model="page"
                          class="my-4"
                          cols="12" sm="6" md="4"
                          :length="numberOfPages"
                          @input="onPageChanged">
            </v-pagination>
          </v-container>
        </v-col>
      </v-row>
    </v-container>
    <v-row>
      <v-col cols="5">
        Number of rows per page:
      </v-col>
      <v-col cols="2">
        <v-card class="d-flex" mr-10 flat tile cols="1">
          <v-text-field dense
                        v-model.lazy.number="newPageSize"
                        @keypress.enter="setPageSize">
          </v-text-field>
        </v-card>
      </v-col>
    </v-row>

    <!-- ################ DIALOGS ############################# -->
    <!--<v-dialog v-model="dialogDelete" max-width="550px">
  <v-card>
    <v-card-title class="headline">Are you sure you want to delete the patient?</v-card-title>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="closeDelete">Cancel</v-btn>
      <v-btn color="blue darken-1" text @click="deleteItemConfirm">OK</v-btn>
      <v-spacer></v-spacer>
    </v-card-actions>
  </v-card>-->
    <!-- </v-dialog>-->
  </div>
</template>

<script>
import PatientService from '@/api-services/patient.service'
export default {
  name: 'PatientList',
  data: () => ({
    page: 1,
    pageSize: 10,
    newPageSize: '',
    numberOfPages: 15,
    totalPatients: 150,
    sortByString: '',
    ascending: 0,
    searchByString: '',

    dialog: false,
    dialogDelete: false,
    selectedOwnerId: 0,
    alertModalTitle: '',
    alertModalContent: '',

    patients: [],
    editedIndex: -1,

    editedItem: {
      name: '',
      calories: 0,
      fat: 0,
      carbs: 0,
      protein: 0
    },
    defaultItem: {
      name: '',
      calories: 0,
      fat: 0,
      carbs: 0,
      protein: 0
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New patient' : 'Edit patient'
    }
  },

  watch: {
    dialog (val) {
      val || this.close()
    },
    dialogDelete (val) {
      val || this.closeDelete()
    }
  },
  created () {
    this.initialize('')
  },

  methods: {
    initialize (params) {
      PatientService.getAll(params).then((response) => {
        this.patients = response.data
        this.page = this.patients.pageIndex + 1
        this.pageSize = this.patients.pageSize
        this.totalPatients = this.patients.total
        this.countNumberOfPages()
        console.log(this.searchByString)
      })
    },
    sortBy (prop) {
      this.sortByString = prop
      this.ascending = (this.ascending + 1) % 3
      this.initialize('pageSize=' + this.pageSize + '&pageIndex=' + (this.page - 1) + '&orderBy=' +
        (this.ascending === 0 ? 'id' : this.sortByString) +
        (this.ascending === 2 ? '&order=desc' : '&order=asc') +
        '&search=' + this.searchByString)
    },
    searchBy () {
      this.initialize('pageSize=' + this.pageSize + '&pageIndex=' + (this.page - 1) + '&orderBy=' +
        (this.ascending === 0 ? 'id' : this.sortByString) +
        (this.ascending === 2 ? '&order=desc' : '&order=asc') +
        '&search=' + this.searchByString)
    },
    onPageChanged () {
      this.initialize('pageSize=' + this.pageSize + '&pageIndex=' + (this.page - 1) + '&orderBy=' +
        (this.ascending === 0 ? 'id' : this.sortByString) +
        (this.ascending === 2 ? '&order=desc' : '&order=asc') +
        '&search=' + this.searchByString)
    },
    setPageSize () {
      this.pageSize = this.newPageSize
      this.newPageSize = ''
      this.page = 1
      this.onPageChanged()
    },
    countNumberOfPages () {
      this.numberOfPages = Math.ceil(this.totalPatients / this.pageSize)
    },
    showDetail (item) {
      this.$router.push({ name: 'PatientDetail', params: { id: item.id } })
    },
    createPatient () {
      console.log(this.page)
      console.log(this.patients.total)
      console.log(this.patients.pageSize)
    }
    // ,
    // editItem (item) {
    //  this.editedIndex = this.patients.data.indexOf(item)
    //  this.editedItem = Object.assign({}, item)
    //  this.dialog = true
    // },
    // deleteItem (item) {
    //  this.editedIndex = this.patients.data.indexOf(item)
    //  this.editedItem = Object.assign({}, item)
    //  this.dialogDelete = true
    //  this.selectedOwnerId = item.id
    // },
    // deleteItemConfirm () {
    //  this.patients.data.splice(this.editedIndex, 1)
    //  this.closeDelete()
    //  PatientService.delete(this.selectedOwnerId)
    //    .then(() => {
    //      this.alertModalTitle = 'Successfully'
    //      this.alertModalContent = 'Successfully deleted Account Owner'
    //      this.$refs.alertModal.show()
    //      this.initialize()
    //    }).catch((error) => {
    //      this.alertModalTitle = 'Error'
    //      this.alertModalContent = error.response.data
    //      this.$refs.alertModal.show()
    //    })
    // },
    // close () {
    //  this.dialog = false
    //  this.$nextTick(() => {
    //    this.editedItem = Object.assign({}, this.defaultItem)
    //    this.editedIndex = -1
    //  })
    // },
    // closeDelete () {
    //  this.dialogDelete = false
    //  this.$nextTick(() => {
    //    this.editedItem = Object.assign({}, this.defaultItem)
    //    this.editedIndex = -1
    //  })
    // },
    // save () {
    //  if (this.editedIndex > -1) {
    //    Object.assign(this.patients.data[this.editedIndex], this.editedItem)
    //  } else {
    //    this.patients.data.push(this.editedItem)
    //  }
    //  this.close()
    // }
  }
}
</script>
