import axios from 'axios';

axios.defaults.baseURL = process.env.REACT_APP_API_BASE_URL;

//response
axios.interceptors.response.use(response => {
  return response;
},
  error => {
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
