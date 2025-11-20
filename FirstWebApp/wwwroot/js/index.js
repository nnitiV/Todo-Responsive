document.addEventListener('DOMContentLoaded', () => {
    const toggleBtn = document.getElementById('theme-toggle');
    const icon = document.getElementById('theme-icon');
    const html = document.documentElement;

    const updateIcon = (theme) => {
        if (theme === 'dark') {
            icon.className = 'bi bi-sun-fill fs-5';
        } else {
            icon.className = 'bi bi-moon-stars-fill fs-5';
        }
    };

    const currentTheme = html.getAttribute('data-bs-theme');
    updateIcon(currentTheme);

    toggleBtn.addEventListener('click', () => {
        const currentTheme = html.getAttribute('data-bs-theme');
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';

        html.setAttribute('data-bs-theme', newTheme);
        localStorage.setItem('theme', newTheme);

        updateIcon(newTheme);
    });
});