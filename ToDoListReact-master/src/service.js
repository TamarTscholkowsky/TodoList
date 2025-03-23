import axios from 'axios';


require('dotenv').config();
// require('dotenv').config();
// axios.defaults.baseURL 
axios.defaults.baseURL =process.env.REACT_APP_API_URL
// const apiUrl = 'https://todolistserver1-uu5v.onrender.com';
//response
// הוספת אינטרספטור לתגובות
axios.interceptors.response.use(response => {
  // החזרת התגובה במידה ואין שגיאות
  return response;
}, error => {
  // רישום השגיאה ללוג
  console.error('Error occurred:', error);

  return Promise.reject(error);
});

export default {
  getTasks: async () => {
    debugger
    try {
      const result = await axios.get(`/tasks`)
      return result.data;
    }
    catch (err) {
      console.log(err)
    }
  },

  addTask: async (name) => {

    console.log('addTask', name)
    try {
      const result = await axios.post(`/${name}`);
      return result
    }
    catch (err) {
      console.log(err)
    }
  },
  // MapPut("/tasks/{id}"
  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    try {
      const result = await axios.put(`/tasks/${id}/${isComplete}`, { isComplete })
      return result
    }
    catch (err) {
      console.log(err)
    }

  },

  deleteTask: async (id) => {
    try {
      const result = await axios.delete(`/tasks/${id}`)
      return result.data
    }
    catch (err) {
      console.log(err)
    }

  }
};
