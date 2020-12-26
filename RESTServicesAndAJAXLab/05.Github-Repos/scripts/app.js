function loadRepos() {
	let ulElement = document.getElementById('repos');
	let inputName = document.getElementById('username').value;

	let url = `https://api.github.com/users/${inputName}/repos`;

	fetch(url)
		.then(res => res.json())
		.then(data=>{
			
			ulElement.innerHTML = data.map(repo=> `<li><a href="${repo.html_url}">${repo.full_name}</a></li>`).join('');
		})
		.catch(err=>{
			ulElement.innerHTML = `No repos from such user found!`;
		});
}