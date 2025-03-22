// Import the required modules
// import express from "express"; // Import the Express framework for building web applications
// import axios from "axios"; // Import Axios for making HTTP requests
const axios = require('axios');
const express = require('express');

const app = express();
const PORT = process.env.PORT || 3000;
const RENDER_API_KEY = 'rnd_NGFcDedXGwlIio3pJx3VUY2iQ3vG';
const RENDER_API_URL = 'https://api.render.com/v1/services';

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
});
