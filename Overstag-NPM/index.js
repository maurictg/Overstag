const express = require('express');
const path = require('path');
const bodyParser = require('body-parser');

const app = express();

app.use(bodyParser.urlencoded({extended: true}));
//app.use(bodyParser.json());

app.use(express.static('public'));

app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'demo.html'));
});

app.get('/test', (req, res) => {
    res.json({status: 'success', data: 'hoi'});
});

app.get('/error', (req, res) => {
    res.status(500).json({error: 'Iets is fout'});
})

app.post('/post', (req, res) => {
    console.log(req.body);
    res.json({message: 'OK', data: req.body});
})

app.get('/notfound', (req, res) => {
    res.status(404).json({error: 'Not found 404'});
})

app.listen(8000);