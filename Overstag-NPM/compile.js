const uglifycss = require('uglifycss');
const fs = require('fs');
const terser = require('terser');


let files = ['main.js', 'selector.js', 'helpers.js'];

let code = '';
files.forEach((f) => code += `${fs.readFileSync(`./public/js/${f}`, 'utf-8')}\n\n`);

let t = terser.minify({'hquery.js':code});

if(!t.error) {
  fs.writeFileSync('../Overstag/wwwroot/js/hquery.min.js', t.code);
  console.log('Done! file: hquery.min.js');
} else {
  console.log(t.error);
}

//Minify CSS
var materialize = uglifycss.processFiles(
  [ './css/overstag-materialize.css' ],
  { maxLineLen: 5000, expandVars: true }
);

var overstag = uglifycss.processFiles(
  ['../Overstag/wwwroot/css/overstag.css'],
  { maxLineLen: 2500, expandVars: true }
);

fs.writeFileSync('../Overstag/wwwroot/css/overstag.min.css', overstag);
fs.writeFileSync('../Overstag/wwwroot/css/overstag-materialize.min.css', materialize);

console.log('Done! file: overstag.min.css, overstag-materialize.min.css');

//Minify JS
let overstagjs = fs.readFileSync('../Overstag/wwwroot/js/overstag.js', 'utf-8');

let o = terser.minify({'overstag.js':overstagjs});

if(!o.error) {
  fs.writeFileSync('../Overstag/wwwroot/js/overstag.min.js', o.code);
  console.log('Done! file: overstag.min.js');
} else {
  console.log(t.error);
}