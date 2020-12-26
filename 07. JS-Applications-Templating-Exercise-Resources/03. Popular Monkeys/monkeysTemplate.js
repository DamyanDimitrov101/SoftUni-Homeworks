const elements = {
    allMonkeys: () => document.querySelector('div.monkeys'),
}

renderMonkeys();

function renderMonkeys() {
    getTemplate()
        .then(monkeyTempl => {
            let template = Handlebars.compile(monkeyTempl);
            let resHTML = template({ monkeys });

            elements.allMonkeys().innerHTML = resHTML;
            elements.allMonkeys().addEventListener('click', loadInfo);
        })
}

function loadInfo(e) {
    if (e.target.tagName === "BUTTON") {
        let pInfo = e.target.parentElement.querySelector('p');

        if (pInfo.style.display === 'none') {
            pInfo.style.display = 'block';
            e.target.textContent = 'Hide';
        }else{
            pInfo.style.display = 'none';
            e.target.textContent = 'Info';
        }
    }
}

function getTemplate() {
    return fetch('./Templates/monkeyTemplate.hbs').then(r => r.text());
}