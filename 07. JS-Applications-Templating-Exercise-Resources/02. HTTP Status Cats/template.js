const elements = {
    sectionForCats: () => document.getElementById('allCats'),
}

renderCatTemplate();

function renderCatTemplate() {
    Promise.all([
        getTemplate('catTemplate'),
        getTemplate('catsTemplate')
    ])
            .then(([cat,tempCats])=> {
                Handlebars.registerPartial('cat', cat);
                let template = Handlebars.compile(tempCats);
                let catsHTML = template({cats});
                elements.sectionForCats().innerHTML = catsHTML;
                elements.sectionForCats().addEventListener('click', onClickBtn);
            });
}

function onClickBtn(e) {
    if (e.target.tagName === 'BUTTON' && e.target.classList.contains('showBtn')) {
        let divToReveal = e.target.parentElement.querySelector('div.status');

        if (divToReveal.style.display === 'none') {
            divToReveal.style.display = 'block';
            e.target.textContent = 'Hide status code';
        }else{
            divToReveal.style.display = 'none';
            e.target.textContent = 'Show status code';
        }
    }

}


function getTemplate(tempName) {
    return fetch(`./Templates/${tempName}.hbs`).then(r => r.text());
}


