const baseUrl = 'https://testapp-c8731.firebaseio.com/students/';
let counter = 2;

const htmlSelectors = {
    createBtn: () => document.getElementById('createStudentBtn'),
    showBtn: () => document.getElementById('showStudentsBtn'),
    tbodyElement: () => document.querySelector('table tbody'),
    error: () => document.getElementsByClassName('errrorLog')[0],
    form: () => document.getElementsByClassName('createStudent')[0],
    inputs: () => document.querySelectorAll('input')
}

htmlSelectors.showBtn()
    .addEventListener('click', showStudents);
htmlSelectors.createBtn()
    .addEventListener('click', createStudent);


function showStudents() {
    fetch(baseUrl + '.json')
        .then(res => res.json())
        .then(data => renderStudents(data))
        .catch(handleError);
}

function createStudent(e) {
    e.preventDefault();

    let form = htmlSelectors.form();
    form.style.display = 'block';

    let submitBtn = document.getElementById('submit');

    
    submitBtn.addEventListener('click', (e) => {
        e.preventDefault();
        
        try {
            let [firstName, lastName, facultyNumber, grade] = htmlSelectors.inputs();
            
        
            if (firstName.value != '' && lastName.value != '' && facultyNumber.value != '' && grade.value != '') {
                let config = {
                    method: "POST",
                    body: JSON.stringify({ id: ++counter, firstName: firstName.value, lastName: lastName.value, facultyNumber: facultyNumber.value, grade: grade.value })
                };

                fetch(baseUrl + '.json', config).then(showStudents);
            
            } else {
                let param = [firstName, lastName, facultyNumber, grade].find(p => p.value == '');
                throw new Error(`${param.previousElementSibling.textContent} must not be empty!`);
            }
        } catch (error) {
            handleError(error);
        }

    });

    let cancelBtn = document.getElementById('cancelBtn');

    
}

function renderStudents(data) {
    let tbody = htmlSelectors.tbodyElement();
    tbody.innerHTML = '';

    let counter = 1;

    if (data[0] === null) {
        data.shift();
    }

    let sorted = Array.from(Object.keys(data)).sort((a, b) => data[a].id - data[b].id);

    sorted.forEach((key) => {
        let { firstName, lastName, facultyNumber, grade, id } = data[key];

        console.log(data[key]);
        let row = createElement('tr', '', {}, {},
            createElement('td', id, {}, {}),
            createElement('td', firstName, {}, {}),
            createElement('td', lastName, {}, {}),
            createElement('td', facultyNumber, {}, {}),
            createElement('td', grade, {}, {}),
        );

        tbody.appendChild(row);
    })
}

function createElement(type, text, attributes, events, ...children) {
    let element = document.createElement(type);

    if (text != '') {
        element.textContent = text;
    }

    if (attributes != {}) {
        Object.entries(attributes).forEach(([key, value]) => {
            element.setAttribute(key, value);
        })
    }

    if (events != {}) {
        Object.entries(events).forEach(([key, value]) => {
            element.addEventListener(key, value);
        })
    }

    element.append(...children);

    return element;
}


function handleError(e) {
    let errorLog = htmlSelectors.error();

    errorLog.textContent = e.message;
    errorLog.style.display = 'block';

    setTimeout(() => { errorLog.style.display = 'none' }, 3000);
}
