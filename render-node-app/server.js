
import axios from "axios"; // Import Axios for making HTTP requests
import express from 'express'
import dotenv from 'dotenv'

dotenv.config();
const RENDER_API_KEY = process.env.RENDER_API_KEY;
const app = express();
const PORT = process.env.PORT || 3000;
// Define a GET endpoint at  ('/') that indicating that the server is running
// app.get('/', (req, res) => res.send('Render service application is running'));
// יצירת endpoint של GET

app.get('/', async (req, res) => {
  try {
    const { data } = await axios.get("https://todolistrender-node.onrender.com", {
      headers: { Authorization: `Bearer ${RENDER_API_KEY}` },
    });
    res.json(data);
  } 
  catch (error) {
    console.error('Error fetching apps from Render:', error.message);
    res.status(500).json({ error: 'Failed to fetch apps from Render' });
  }
});

// הפעלת השרת
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
  // console.log('Render API URL:', RENDER_API_URL);
   console.log('Render API Key:', RENDER_API_KEY ? 'Loaded' : 'Not loaded');

});







