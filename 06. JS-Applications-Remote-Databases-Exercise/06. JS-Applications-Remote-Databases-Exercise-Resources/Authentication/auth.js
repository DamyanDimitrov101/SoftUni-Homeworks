
const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});

const htmlSelectors = {
    submitSignUpBtn: () => document.getElementById('singUpBtn'),
    signInBtn: () => document.getElementById('signInBtn'),   
    signUpEmail:    ()=> document.getElementById('emailCreateId'),
    signUpPassword:()=> document.getElementById('passwordCreateId'),
    signInEmail: ()=> document.getElementById('emailSignId'),
    signInPassword: ()=> document.getElementById('passwordSignId'),
    nameSignUpElement: ()=> document.getElementById('name')
}

htmlSelectors.submitSignUpBtn()
    .addEventListener('click',createUser);
htmlSelectors.signInBtn()
    .addEventListener('click',signIn);


function createUser(e) {
    let email = htmlSelectors.signUpEmail();
    let password = htmlSelectors.signUpPassword();
    let name = htmlSelectors.nameSignUpElement();

    if (email.value!='') {
        if (password.value>=6) {
            firebase.auth().createUserWithEmailAndPassword(email.value,password.value)
                .then(res=> {alert('Succesfully created user!'); email.value=''; password.value=''; name.value=''})
                .catch((e)=> alert(e.message));
        }else{
            alert('Password must be atleast 6 characters!');
        }
    }else{
        alert('Email must not be empty!');
    }
}

function signIn(e) {
    let email = htmlSelectors.signInEmail();
    let password = htmlSelectors.signInPassword();

    if (email.value!='') {
        if (password.value>=6) {
            firebase.auth().signInWithEmailAndPassword(email.value,password.value)
                .then(res=> {alert('Succesfully logged user!'); email.value=''; password.value='';})
                .catch((e)=> alert(e.message));
        }else{
            alert('Password must be atleast 6 characters!');
        }
    }else{
        alert('Email must not be empty!');
    }
}