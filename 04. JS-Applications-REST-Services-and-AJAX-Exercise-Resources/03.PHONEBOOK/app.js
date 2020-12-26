function attachEvents() {
    const loadBtn = document.getElementById('btnLoad');
    const ulPhonebookElement = document.getElementById('phonebook');
    const createBtn = document.getElementById('btnCreate');

    let url = 'https://phonebook-nakov.firebaseio.com/phonebook.json';

    loadBtn.addEventListener('click', () => {
        fetch(url)
            .then(response => response.json())
            .then(data => {
                ulPhonebookElement.innerHTML =
                    Array.from(Object.keys(data))
                        .filter(p => data[p].person !== '')
                        .map(p => `<li id="${p}">${data[p].person}: ${data[p].phone} <button id="btnDelete">Delete</button></li>`)
                        .join('');

            })
        ulPhonebookElement.addEventListener('click', clickDelete);
    })

    createBtn.addEventListener('click', () => {
        let inputPerson = document.getElementById('person');
        let inputPhone = document.getElementById('phone');

        let newPerson = { person: inputPerson.value, phone: inputPhone.value };

        fetch(url, {            
            method: "POST", 
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify(newPerson)
        });

        inputPerson.value = '';
        inputPhone.value = '';
    })





    function clickDelete(e) {
        if (e.target.type === 'submit') {
            let liToDelete = e.target.parentElement;

            let urlToDelete = `https://phonebook-nakov.firebaseio.com/phonebook/${liToDelete.id}.json`;

            fetch(urlToDelete, { method: "DELETE" });
        }

    }
}

attachEvents();