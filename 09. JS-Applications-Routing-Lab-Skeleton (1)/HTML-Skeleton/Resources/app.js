const baseURL = 'https://testapp-c8731.firebaseio.com/furniture.json';

const elements = {
    container: () => document.getElementById('container'),
    notifications: () => document.getElementById('notifications'),
    nav: () => document.getElementById('nav'),
    form: () => document.querySelector('form'),
    createBtn: ()=> document.getElementsByClassName('btn-primary')[0],
    errorLog: ()=> document.getElementById('errorLog'),
    root: ()=> document.getElementById('root'),
    details: () => document.getElementById('details'),
    profileRoot: () => document.getElementById('profileRoot'),

};

const pathMap ={
    'home': document.getElementById('home'),
    'create': document.getElementById('createFurniture'),
    'details': document.getElementById('details'),
    'profile': document.getElementById('profile'),
    'delete': document.getElementById('profile'),
}

const router = (pathName,id) => {
    console.log(pathName);

    Object.keys(pathMap).forEach(path=> {
        pathMap[path].style.display = 'none';            
    });
    
    pathMap[pathName].style.display = 'block';
    
    switch (pathName) {
        case 'home':
            renderHomePage();
            break;
        case 'details':      
            renderDetails(id);
            break;
        case 'profile':
            renderProfile();
            break;
        case 'delete':
            renderDelete(id);
            break;
    }
}

elements.nav().addEventListener('click',onRouteChange)
elements.createBtn().addEventListener('click',createFurniture);



function renderHomePage() {
    Promise.all([
        fetch(baseURL)
        .then(res=> res.json()),
        fetch('/Templates/furnitureTemplate.hbs')
        .then(res=> res.text())
    ])
        .then(([data,temp])=> {      
            if (data[0] === null) {
                data.shift();
            }

            Object.keys(data).forEach(x=> {
                data[x].id = x;
            });
            
            let template = Handlebars.compile(temp);
            let result = template({data});


            elements.root().innerHTML = result;
        });
}

function createFurniture(e) {
    e.preventDefault();

    let [make,model,year,description,price,image,material] = elements.form().querySelectorAll('input');

    try {
        validateParams(make,model,year,description,price,image,material);
    
        let furniture = {
            make: make.value,
            model: model.value,
            year: year.value,
            description: description.value,
            price: price.value,
            image: image.value,
            material: material.value!='' ? material.value : undefined,
        };
    
        const config = {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(furniture),
        }
    
        fetch(baseURL,config)
            .then(res=> res.json())
            .then(data=> redirect('home'));
    } catch (error) {
        handleError(error);
    }
}

function renderDetails(id) {
    fetch(`https://testapp-c8731.firebaseio.com/furniture/${id}.json`)
        .then(res=> res.json())
        .then(data=> {
            let details = elements.details();
            details.querySelector('img').src = data.image;
            let [make,model,year,description,price,material] = details.querySelectorAll('p');

            make.textContent = data.make;
            model.textContent = data.model;
            year.textContent = data.year;
            description.textContent = data.description;
            price.textContent = data.price;
            material.textContent = data.material;

        });
}

function renderProfile() {
    Promise.all([
        fetch('./Templates/profileFurnTemplate.hbs').then(res=> res.text()),
        fetch(baseURL).then(res=> res.json())
    ])
        .then(([temp,data])=> {
            if (data[0] === null) {
                data.shift();
            }

            Object.keys(data).forEach(x=> {
                data[x].id = x;
            });

            let template = Handlebars.compile(temp);
            let result = template({data});

            elements.profileRoot().innerHTML = result;
        })
}

function renderDelete(id) {
    let config = {
        method: "DELETE",
        headers: {'content-type': 'application/json'},
    }

    fetch(`https://testapp-c8731.firebaseio.com/furniture/${id}.json`,config)
        .then(renderProfile);
}

function redirect(path,id) {
    history.pushState({},'',path);
    
    router(path,id);
}

function onRouteChange(e) {
        if (e.target.tagName != 'A') {
            return;
        }
        
        e.preventDefault();

        
        var path = e.target.href.substring(e.target.href.lastIndexOf('/')+1);
     
        redirect(path);
}

function validateParams(make,model,year,description,price,image,material) {
    if (make.value == '' || make.value.length > 30 || make.value.length < 4) {
        throw new Error('Invalid make input!');
    }

    if (model.value == '' || model.value.length > 30 || model.value.length < 4) {
        throw new Error('Invalid model input!');
    }

    if (year.value == '' || year.value > 2022 || year.value < 1950) {
        throw new Error('Invalid year input!');
    }

    if (description.value == '' ||  description.value.length > 30000 || description.value.length < 10) {
        throw new Error('Invalid description input!');
    }

    if (price.value == '' || price.value < 0) {
        throw new Error('Invalid price input!');
    }

    if (image.value == '') {
        throw new Error('Invalid image input!');
    }

    if (material.value.length > 300) {
        throw new Error('Invalid material input!');
    }
}

function handleError(e) {
    elements.errorLog().querySelector('p').textContent = e.message;
    elements.errorLog().style.display = 'block';

    setTimeout(()=> {elements.errorLog().style.display = 'none'},3000);
}

let [path, id] = location.pathname.split('/').filter(x=>x);
redirect(path,id);
