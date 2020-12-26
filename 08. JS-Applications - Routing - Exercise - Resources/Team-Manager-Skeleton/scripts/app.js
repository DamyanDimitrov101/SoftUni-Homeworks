const userModule = firebase.auth();
const baseUrl = 'https://testapp-c8731.firebaseio.com/teams.json';

const router = Sammy('#main', function () {

    this.use('Handlebars', 'hbs');

    //GET
    this.get('/home', function (context) {
        checkForUser(context);

        loadPartials(context)
            .then(function () {
                this.partial('./templates/home/home.hbs');
            });
    });

    this.get('/about', function (context) {
        checkForUser(context);

        loadPartials(context)
            .then(function () {
                this.partial('./templates/about/about.hbs');
            });
    });

    this.get('/login', function (context) {
        this.loadPartials({
            'header': './templates/common/header.hbs',
            'footer': './templates/common/footer.hbs',
            'loginForm': './templates/login/loginForm.hbs'
        })
            .then(function () {
                this.partial('./templates/login/loginPage.hbs');
            });
    });

    this.get('/register', function (context) {

        this.loadPartials({
            'header': './templates/common/header.hbs',
            'footer': './templates/common/footer.hbs',
            'registerForm': './templates/register/registerForm.hbs'
        })
            .then(function () {
                this.partial('./templates/register/registerPage.hbs');
            });
    });

    this.get('/logout', function (contex) {
        userModule.signOut()
            .then((res) => {
                localStorage.removeItem('userInfo');
                this.redirect('/home');
                handleInfo('User signed out!');
            }).catch(function (error) {
                handleError(error.message)
            });
    });

    this.get('/catalog', function (context) {
        checkForUser(context);

        fetch(baseUrl)
            .then(res => res.json())
            .then(data => {
                if (data == null) {
                    context.hasNoTeam = true;
                } else {

                    Object.keys(data).forEach(tId => {
                        data[tId].id = tId;
                    });

                    context.teams = Object.keys(data)
                        .map(tId => data[tId]);
                }


                this.loadPartials({
                    'header': './templates/common/header.hbs',
                    'footer': './templates/common/footer.hbs',
                    'team': './templates/catalog/team.hbs',
                })
                    .then(function (context) {
                        this.partial('./templates/catalog/teamCatalog.hbs');
                    });
            });

    });

    this.get('/create', function (context) {
        checkForUser(context);

        this.loadPartials({
            'header': './templates/common/header.hbs',
            'footer': './templates/common/footer.hbs',
            'createForm': './templates/create/createForm.hbs',
        })
            .then(function (context) {

                this.partial('./templates/create/createPage.hbs');
            });
    })

    this.get('/catalog/:teamId', function (context) {
        let id = context.params.teamId.substr(1);

        checkForUser(context);

        fetch(`https://testapp-c8731.firebaseio.com/teams/${id}.json`)
            .then(res => res.json())
            .then(data => {
                if (data != null) {
                    let user = checkForUser(context);

                    context.name = data.name;
                    context.comment = data.comment;
                    context.teamId = id;

                    if (data.members && data.members.length > 0) {
                        console.log(data.members);
                        context.members = data.members;
                    }

                    console.log(user.teamId);
                    if (user.teamId != undefined) {
                        context.hasNoTeam = false;
                        context.isOnTeam = true;


                        if (user.teamId === id) {
                            context.isAuthor = true;
                        }
                    }

                    this.loadPartials({
                        'header': './templates/common/header.hbs',
                        'footer': './templates/common/footer.hbs',

                        'teamMember': '../templates/catalog/teamMember.hbs',
                        'teamControls': '../templates/catalog/teamControls.hbs'
                    })
                        .then(function (context) {

                            this.partial('../templates/catalog/details.hbs');
                        });
                }
            });

    });

    this.get('/join/:teamId', function (context) {
        let id = context.params.teamId.substr(1);

        let user = JSON.parse(localStorage.getItem('userInfo'));
        user.teamId = id;

        localStorage.setItem('userInfo', JSON.stringify(user));


        fetch(`https://testapp-c8731.firebaseio.com/teams/${id}.json`)
            .then(res => res.json())
            .then(data => {
                let members = [];
                if (data.members == undefined) {
                    members = data.members = [{ email: user.email }];
                } else {
                    members = data.members.push({ email: user.email });
                }

                fetch(`https://testapp-c8731.firebaseio.com/teams/${id}.json`, { method: "PATCH", body: JSON.stringify({ members }) })
                    .then(res => {

                        handleInfo('Succesfully joined team!');
                        this.redirect(`/catalog/:${user.teamId}`);
                    });
            });

    })

    this.get('/leave', function (context) {
        let user = checkForUser(context);
        let teamId = user.teamId;

        user.teamId = undefined;


        localStorage.setItem('userInfo', JSON.stringify(user));


        fetch(`https://testapp-c8731.firebaseio.com/teams/${teamId}.json`)
            .then(res => res.json())
            .then(data => {
                let members = data.members.filter(x => x.email != user.email);
                console.log(members);

                fetch(`https://testapp-c8731.firebaseio.com/teams/${teamId}.json`, { method: "PATCH", body: JSON.stringify({ members }) })
                    .then(res => {


                        handleInfo('Team left!');
                        this.redirect(`/catalog/:${teamId}`);
                    });
            });
    })

    this.get('/edit/:teamId', function (context) {
        let id = context.params.teamId.substr(1);
        let user = checkForUser(context);

        fetch(`https://testapp-c8731.firebaseio.com/teams/${id}.json`)
            .then(res => res.json())
            .then(data => {
                context.name = data.name;
                context.comment = data.comment;
                context.teamId = id;

                this.loadPartials({
                    'header': './templates/common/header.hbs',
                    'footer': './templates/common/footer.hbs',
                    'editForm': '../templates/edit/editForm.hbs',
                })
                .then(function (context) {
                    this.partial('../templates/edit/editPage.hbs');
                })
            })

    })

    //POST

    this.post('/register', function (context) {
        let { email, password, repeatPassword } = context.params;

        try {
            if (email == '') {
                throw new Error('Email must not be empty!');
            }

            if (password !== repeatPassword) {
                throw new Error('Password do not match!');
            }

            userModule.createUserWithEmailAndPassword(email, password)
                .then((data) => {
                    this.redirect('/login');

                    handleInfo('User created!');
                })
                .catch((error) => {
                    var errorMessage = error.message;
                    handleError(errorMessage);
                });
        } catch (error) {
            handleError(error.message);
        }
    })

    this.post('/login', function (context) {
        let { email, password } = context.params;

        userModule.signInWithEmailAndPassword(email, password)
            .then(({ user }) => {
                let { email, uid } = user;

                localStorage.setItem('userInfo', JSON.stringify({ uid, email }));

                this.redirect('/home');
                handleInfo('Welcome!');
            })
            .catch((error) => {
                var errorCode = error.code;
                var errorMessage = error.message;

                handleError(errorMessage);
            });
    });

    this.post('/create', function (context) {
        let { name, comment } = context.params;


        let user = checkForUser(context);

        if (name == '') {
            handleError('The name must not be empty!');
            return;
        }

        let members = [{ email: user.email }];

        let config = {
            method: "POST",
            body: JSON.stringify({ name, comment, members })
        };

        fetch(baseUrl, config)
            .then(res => res.json())
            .then(res => {
                user.teamId = res.name;

                localStorage.setItem('userInfo', JSON.stringify(user));

                handleInfo('Team created!');
                this.redirect('/catalog');
            })
            .catch(e => handleError(e.message));
    })

    this.post('/edit/:teamId', function (context) {
        console.log(context);
        let { name, comment , teamId} = context.params;
        teamId = teamId.substr(1);
        if (name == '') {
            handleError('The name must not be empty!');
            return;
        }

        let config = {
            method: "PATCH",
            body: JSON.stringify({ name, comment})
        };

        fetch(`https://testapp-c8731.firebaseio.com/teams/${teamId}.json`,config)
            .then(res=> {
                handleInfo('Edited succesfully!');
                this.redirect(`./catalog/:${teamId}`);
            })

    })
});


function loadPartials(context) {
    return context.loadPartials({
        'header': './templates/common/header.hbs',
        'footer': './templates/common/footer.hbs'
    })
}

function handleInfo(message) {
    let info = document.getElementById('infoBox');
    info.style.display = 'block';
    info.textContent = message;

    setTimeout(() => { info.style.display = 'none'; }, 3000)
}

function handleError(message) {
    let errorLog = document.querySelector('#errorBox');
    errorLog.style.display = 'block';
    errorLog.textContent = message;

    setTimeout(() => { errorLog.style.display = 'none' }, 3000);
}

function checkForUser(context) {
    let user = JSON.parse(localStorage.getItem('userInfo'));

    if (user != undefined) {
        context.loggedIn = true;
        context.email = user.email;
    }

    return user;
}

(() => {
    router.run('/home');
})();