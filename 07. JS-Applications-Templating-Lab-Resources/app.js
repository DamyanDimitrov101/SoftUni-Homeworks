import Handlebars from "https://jspm.dev/handlebars";

showContacts();

function showContacts() {
    let contactsElement = document.getElementById('contacts');
    
    Handlebars.registerPartial('contactPartial',contactView);

    let contactsView = document.getElementById('contacts-template').innerHTML;

    let contactsTemplate = Handlebars.compile(contactsView);
    
    contactsElement.innerHTML = contactsTemplate({contacts});

    contactsElement.addEventListener('click',(e) =>{
        if (e.target.tagName === 'BUTTON') {
            showDetails(e);
        }
    })
}

function showDetails(e) {
    let contactDiv = e.target.nextElementSibling;

    if (contactDiv.style.display == 'none') {
        contactDiv.style.display = 'block';
    }else{
        contactDiv.style.display = 'none';
    }
}
