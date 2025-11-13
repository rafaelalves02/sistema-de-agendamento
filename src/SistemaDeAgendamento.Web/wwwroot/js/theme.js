// Sistema de Dark Mode
(function () {
    'use strict';

    const themeToggle = document.getElementById('themeToggle');
    const html = document.documentElement;

    // Verificar preferência salva ou do sistema
    function getTheme() {
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme) {
            return savedTheme;
        }
        // Verificar preferência do sistema
        if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            return 'dark';
        }
        return 'light';
    }

    // Aplicar tema
    function setTheme(theme) {
        html.setAttribute('data-theme', theme);
        localStorage.setItem('theme', theme);
        updateThemeIcon(theme);
    }

    // Atualizar ícone do toggle
    function updateThemeIcon(theme) {
        if (themeToggle) {
            const icon = themeToggle.querySelector('i');
            if (icon) {
                if (theme === 'dark') {
                    icon.classList.remove('bi-moon-stars');
                    icon.classList.add('bi-sun');
                } else {
                    icon.classList.remove('bi-sun');
                    icon.classList.add('bi-moon-stars');
                }
            }
        }
    }

    // Toggle tema
    function toggleTheme() {
        const currentTheme = html.getAttribute('data-theme') || getTheme();
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        setTheme(newTheme);
    }

    // Inicializar tema
    function initTheme() {
        const theme = getTheme();
        setTheme(theme);
    }

    // Event listener para o toggle
    if (themeToggle) {
        themeToggle.addEventListener('click', toggleTheme);
    }

    // Ouvir mudanças na preferência do sistema
    if (window.matchMedia) {
        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
            // Só aplicar se não houver preferência salva
            if (!localStorage.getItem('theme')) {
                setTheme(e.matches ? 'dark' : 'light');
            }
        });
    }

    // Inicializar ao carregar
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initTheme);
    } else {
        initTheme();
    }
})();

