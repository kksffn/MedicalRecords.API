<template>
    <v-app-bar app
               color="indigo" dark>
      <v-row>
        <v-col cols="1">
          <div class="d-flex align-center">
            <!--<a href="/">-->
              <v-img src="@/assets/favicon.png" height="64" contain />
            <!--</a>-->
          </div>
        </v-col>
        <v-col cols="1">
          <div class="d-flex align-center mt-4">
            <v-btn depressed color="primary" href="/">
              Get all patients
            </v-btn>
          </div>
        </v-col>
        <v-col cols="8" class="text-center mt-2">
          <v-toolbar-title class="white--text ml-10
                         display-2 font-weight-boldfont-weight-bold">
            Medical records
          </v-toolbar-title>
        </v-col>
        <v-col cols="1" class="mt-3">
          <div>
            <v-btn color="success">Login</v-btn>
          </div>
        </v-col>
        <v-col cols="1" class="mt-3">
          <v-btn color="info">Register</v-btn>
        </v-col>
      </v-row>
    </v-app-bar>
</template>
