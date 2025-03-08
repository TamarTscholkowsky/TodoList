import axios from 'axios';
axios.defaults.baseURL = 'http://localhost:3000'; 
let apiUrl = axios.defaults.baseURL 
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
    try{
    const result = await axios.get(`${apiUrl}/tasks`)
    return result.data;
    }
    catch(err){
      console.log(err)
    }
  },

  addTask: async(name)=>{

    console.log('addTask', name)
    //TODO
    const result = await axios.post(`${apiUrl}/${name}`)  
      return result 
  },
// MapPut("/tasks/{id}"
  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    //TODO
    const result = await axios.put(`${apiUrl}/tasks/${id}/${isComplete}`, { isComplete })   
    return result 
  
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
    debugger
    const result = await axios.delete(`${apiUrl}/tasks/${id}`)
    return result.data
  }
};
