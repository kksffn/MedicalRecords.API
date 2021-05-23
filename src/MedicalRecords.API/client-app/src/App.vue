<template>
  <v-app>
    <v-system-bar>
      <ul>
        <li></li>
      </ul>
    </v-system-bar>
    <appbar/>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import appbar from '@/components/Appbar'
export default {
  name: 'App',
  components: {
    appbar
  }
}
</script>
