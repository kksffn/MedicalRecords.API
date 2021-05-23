import axios from 'axios'
import config from '@/config/config.js'

export default axios.create({
  baseURL: config.baseURL // 'https://localhost:44370/api'
})
