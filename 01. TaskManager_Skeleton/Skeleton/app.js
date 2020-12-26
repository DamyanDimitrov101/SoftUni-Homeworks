function solve() {
    let openSectionElement = document.getElementsByTagName('section')[1];
    let inProgressSectionElement = document.getElementsByTagName('section')[2];
    let completeSectionElement = document.getElementsByTagName('section')[3];
    let [taskElement, descriptionElement, dateElement] = Array.from(document.querySelector('form').children).filter(e => e.tagName === 'INPUT' || e.tagName === 'TEXTAREA');
    let btnAdd = document.getElementById('add');

    btnAdd.addEventListener('click', addEvent);




    function addEvent(e) {
        e.preventDefault();

        if (!taskElement.value
            || !descriptionElement.value
            || !dateElement.value) {
            return;
        }

        let article = document.createElement('article');
        article.innerHTML = `<h3>${taskElement.value}</h3><p>Description: ${descriptionElement.value}</p><p>Due Date: ${dateElement.value}</p>`;

        let div = document.createElement('div');
        let btnStart = document.createElement('button');
        let btnDelete = document.createElement('button');

        div.className = 'flex';
        btnStart.className = 'green';
        btnDelete.className = 'red';

        btnStart.innerText = 'Start';
        btnDelete.innerText = 'Delete';

        btnStart.addEventListener('click', onStart);
        btnDelete.addEventListener('click', onDelete);

        div.appendChild(btnStart)
        div.appendChild(btnDelete);

        article.appendChild(div);

        openSectionElement.lastElementChild.appendChild(article);

        taskElement.value = '';
        descriptionElement.value = '';
        dateElement.value = '';
    }

    function onStart(e) {
        let articleCurrent = e.currentTarget.parentElement.parentElement;
        
        let divWithBtns = e.currentTarget.parentElement;

        divWithBtns.firstElementChild.remove();
        console.log(divWithBtns);

        let btnFinish = document.createElement('button');
        btnFinish.className = 'orange';
        btnFinish.innerText = 'Finish';
        divWithBtns.appendChild(btnFinish);

        btnFinish.addEventListener('click',onFinish);

        inProgressSectionElement.lastElementChild.appendChild(articleCurrent);
    }

    function onFinish(e) {
        let article = e.currentTarget.parentElement.parentElement;

        article.lastElementChild.remove();

        completeSectionElement.lastElementChild.appendChild(article);
    }

    function onDelete(e) {
        let article = e.currentTarget.parentElement.parentElement;
        
        article.remove();
    }
}