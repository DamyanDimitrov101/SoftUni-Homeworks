const baseUrl = 'https://restcountries.eu/rest/v2/all';
const elements = {
    btnLoad: () => document.getElementById('btnLoadTowns'),
    input: () => document.getElementById('towns'),
    root: () => document.getElementById('root'),
}

elements.btnLoad().addEventListener('click', loadTowns);


function loadTowns(e) {
    e.preventDefault();

    //let towns = splitMulti(elements.input().value, [',', ', '])
      //  .map(t => { return { name: t } });
    Promise.all([
        fetch(baseUrl).then(res => res.json()),
        getTemplate()
    ]).then(([countries,townsTemplate])=> {
        let template = Handlebars.compile(townsTemplate);
        let resultHtml = template({countries});
        elements.root().innerHTML = resultHtml;
    });
}

function getTemplate() {
    return fetch('./Templates/townsTemplate.hbs').then(r => r.text());
}

function splitMulti(str, tokens) {
    var tempChar = tokens[0];
    for (var i = 1; i < tokens.length; i++) {
        str = str.split(tokens[i]).join(tempChar);
    }
    str = str.split(tempChar);
    return str;
}