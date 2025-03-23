// Import the required modules
// import express from "express"; // Import the Express framework for building web applications
// import axios from "axios"; // Import Axios for making HTTP requests
import cors from 'cors'
import express from 'express'
// import https from 'https'
import dotenv from 'dotenv'
// dotenv.config()
// // import axios from 'axios'

// // const axios = require('axios');
// const express = require('express');
dotenv.config()
const app = express();
app.use(cors())
const PORT = process.env.PORT || 3000;
const RENDER_API_KEY = process.env.RENDER_API_KEY;
const RENDER_API_URL = process.env.RENDER_API_URL;

// יצירת endpoint של GET
app.get('/apps', async (req, res) => {
  try {
    // שליחת בקשה ל-Render API
    const response = await axios.get(RENDER_API_URL, {
      headers: {
        Authorization: `Bearer ${RENDER_API_KEY}`,
      },
      
    });
    
    // החזרת רשימת האפליקציות כ-JSON
    res.json(response.data);
  } 
  catch (error) {
    console.error('Error fetching apps from Render:', error.message);
    res.status(500).json({ error: 'Failed to fetch apps from Render' });
  }
});

// הפעלת השרת
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
  console.log('Render API URL:', RENDER_API_URL);
   console.log('Render API Key:', RENDER_API_KEY ? 'Loaded' : 'Not loaded');

});
