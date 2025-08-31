// استخراج پارامترهای URL
function getUrlParams() {
    const params = new URLSearchParams(window.location.search);
    return {
        repo: params.get('repo'),
        mode: params.get('mode'),
        background: params.get('background'),
        title: params.get('title'),
        owner: params.get('owner'),
        description: params.get('description'),
        stats: params.get('stats')
    };
}

// شبیه‌سازی اطلاعات مخزن (در حالت واقعی از API دریافت می‌شود)
function displayRepoInfo() {
    const params = getUrlParams();
    const repoUrl = params.repo;

    if (repoUrl) {
        // استخراج نام مخزن از URL
        const urlParts = repoUrl.split('/');
        const repoName = urlParts[urlParts.length - 1];
        const ownerName = urlParts[urlParts.length - 2];

        // نمایش اطلاعات (در حالت واقعی از API دریافت می‌شود)
        document.getElementById('repoName').textContent = repoName;
        document.getElementById('repoOwner').textContent = ownerName;
        document.getElementById('repoDesc').textContent = 'توضیحات مخزن از GitHub API دریافت می‌شود';
        document.getElementById('fileName').textContent = `Card-${repoName}.png`;

        // آمارهای نمونه
        document.getElementById('starsCount').textContent = '1.2K';
        document.getElementById('forksCount').textContent = '345';
        document.getElementById('issuesCount').textContent = '12';
        document.getElementById('repoLang').textContent = 'C#';
    }
}

function downloadCard() {
    // در حالت واقعی، این فایل از سرور دانلود می‌شود
    alert('در حالت واقعی، فایل PNG از سرور دانلود می‌شود');

    // کد نمونه برای دانلود
    // const link = document.createElement('a');
    // link.href = '/api/download/' + fileName;
    // link.download = fileName;
    // link.click();
}

// اجرای تابع نمایش اطلاعات هنگام بارگذاری صفحه
window.addEventListener('load', function () {
    displayRepoInfo();

    // انیمیشن ورود
    const container = document.querySelector('.container');
    container.style.opacity = '0';
    container.style.transform = 'translateY(30px)';

    setTimeout(() => {
        container.style.transition = 'all 0.6s ease';
        container.style.opacity = '1';
        container.style.transform = 'translateY(0)';
    }, 100);
});

// افکت confetti برای جشن موفقیت
function createConfetti() {
    const colors = ['#ff6b6b', '#4ecdc4', '#45b7d1', '#96ceb4', '#ffeaa7'];

    for (let i = 0; i < 50; i++) {
        setTimeout(() => {
            const confetti = document.createElement('div');
            confetti.style.cssText = `
                    position: fixed;
                    width: 10px;
                    height: 10px;
                    background: ${colors[Math.floor(Math.random() * colors.length)]};
                    top: -10px;
                    left: ${Math.random() * 100}vw;
                    border-radius: 50%;
                    pointer-events: none;
                    z-index: 1000;
                    animation: fall 3s linear forwards;
                `;

            document.body.appendChild(confetti);

            setTimeout(() => confetti.remove(), 3000);
        }, i * 50);
    }
}

// اضافه کردن CSS برای انیمیشن confetti
const style = document.createElement('style');
style.textContent = `
    @keyframes fall {
            to {
                transform: translateY(100vh) rotate(360deg);
                opacity: 0;
            }
        }
    `;
document.head.appendChild(style);

// اجرای confetti هنگام بارگذاری
setTimeout(createConfetti, 500);