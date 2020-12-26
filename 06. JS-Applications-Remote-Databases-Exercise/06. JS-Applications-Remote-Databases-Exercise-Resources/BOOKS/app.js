const baseUrl = 'https://testapp-c8731.firebaseio.com/Books/';

const htmlSelectors = {
    loadBtn: () => document.getElementById('loadBooks'),
    submitBtn: () => document.getElementById('submitBtn'),
    booksList: () => document.querySelector('table > tbody'),
    errorDivElement: () => document.getElementById('errorLog'),
};


htmlSelectors.loadBtn()
    .addEventListener('click', loadBooks);
htmlSelectors.submitBtn()
    .addEventListener('click', createBook);




function loadBooks() {
    fetch(baseUrl + '.json')
        .then(res => res.json())
        .then(renderBooks)
        .catch(handleError);
}


function createBook(e) {
    let [title, author, isbn] = document.querySelectorAll('.createBook > input');

    e.preventDefault();

    let book = { title: title.value, author: author.value, isbn: isbn.value };

    try {
        if (title.value != "" && author.value != "" && isbn.value != "") {
            fetch(baseUrl + '.json', { method: "POST", body: JSON.stringify(book) })
                .then(loadBooks);



        } else {
            const param = [title, author, isbn].find(p => p.value == '');
            throw new Error(`${param.previousElementSibling.textContent.toLowerCase()} must not be empty!`);
        }
    } catch (error) {
        handleError(error);
    }
}

function renderBooks(data) {
    let books = htmlSelectors['booksList']();
    books.innerHTML = '';

    if (data[0] === null) {
        data.shift();
    }
    Object.keys(data)
        .forEach((id) => {
            const { author, isbn, title } = data[id];

            let rowToAppend = createDOMElement('tr', '', {}, {},
                createDOMElement('td', title, {}, {}),
                createDOMElement('td', author, {}, {})
                , createDOMElement('td', isbn, {}, {}),
                createDOMElement('td', '', {}, {},
                    createDOMElement('button', 'Edit', { 'data-id': id }, { 'click': edditElement }),
                    createDOMElement('button', 'Delete', { 'data-id': id }, { 'click': deleteElement })));

            books.appendChild(rowToAppend);
        });
}

function createDOMElement(type, text, attributes, events, ...children) {
    let element = document.createElement(type);

    if (text != '') {
        element.textContent = text;
    }

    if (attributes != {}) {
        Object.entries(attributes)
            .forEach(([key, value]) => {
                element.setAttribute(key, value);
            });
    }

    if (events != {}) {
        Object.entries(events)
            .forEach(([key, value]) => {
                element.addEventListener(key, value);
            });
    }

    element.append(...children);

    return element;
}

function edditElement(e) {
    let id = this.getAttribute('data-id');
    let oldForms = document.getElementsByTagName('form');

    Array.from(oldForms).forEach(old => { old.style.display = 'none'; })

    let newForm = document.getElementsByClassName('editBook')[0];
    newForm.style.display = 'block';

    let [title, author, isbn] = document.querySelectorAll('.editBook > input');

    let buttonEdit = document.querySelectorAll('.editBook button')[0];
    let buttonCancel = document.querySelectorAll('.editBook button')[1];

    buttonEdit.addEventListener('click', (e) => {
        e.preventDefault();
        let urlBook = `${baseUrl}${id}.json`;

        let book = { title: title.value, author: author.value, isbn: isbn.value };

        let config = {
            method: "PATCH",
            body: JSON.stringify(book)
        }

        try {
            if (title.value != "" && author.value != "" && isbn.value != "") {
                fetch(urlBook, config)
                    .then(loadBooks);
    
                returnToCreate(e);
            } else {
                const param = [title, author, isbn].find(p => p.value == '');                
                throw new Error(`${param.previousElementSibling.textContent.toLowerCase()} must not be empty!`);
            }
        } catch (error) {
            handleError(error);
        }
    })

    buttonCancel.addEventListener('click', returnToCreate);
}

function deleteElement(e) {
    let id = this.getAttribute('data-id');
    let oldForms = document.getElementsByTagName('form');

    Array.from(oldForms).forEach(old => { old.style.display = 'none'; })

    let newForm = document.getElementsByClassName('deleteBook')[0];
    newForm.style.display = 'block';
    newForm.style.disabled = 'true';

    let [title, author, isbn] = document.querySelectorAll('.deleteBook > input');

    title.disabled = true;
    author.disabled = true;
    isbn.disabled = true;

    let url = `${baseUrl}${id}.json`;

    fetch(url)
        .then(res => res.json())
        .then(data => {
            title.value = data.title;
            author.value = data.author;
            isbn.value = data.isbn;
        });

    let deleteBtn = document.querySelectorAll('.deleteBook button')[0];
    let cancelBtn = document.querySelectorAll('.deleteBook button')[1];


    deleteBtn.addEventListener('click', (e) => {
        e.preventDefault();

        let config = {
            method: "Delete"
        };

        fetch(url, config)
            .then(loadBooks);


        returnToCreate(e);
    });

    cancelBtn.addEventListener('click', (e) => {
        e.preventDefault();

        returnToCreate(e);
    })
}

function returnToCreate(e) {
    e.preventDefault();

    let newForm = document.getElementsByClassName('createBook')[0];
    newForm.style.display = 'block';

    let oldForm = document.getElementsByClassName('editBook')[0];
    oldForm.style.display = 'none';

    let oldForm2 = document.getElementsByClassName('deleteBook')[0];
    oldForm2.style.display = 'none';

    newForm.querySelector('input[id=title]').value = '';
    newForm.querySelector('input[id=author]').value = '';
    newForm.querySelector('input[id=isbn]').value = '';
}

function handleError(e) {
    let div = htmlSelectors.errorDivElement();
    div.textContent = e.message;
    div.style.display = 'block';

    setTimeout(() => { div.style.display = 'none' }, 3000)
}