// Scripts for firebase and messaging
// importScripts is used in service workers instead of import
importScripts('https://www.gstatic.com/firebasejs/11.6.1/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/11.6.1/firebase-messaging-compat.js');

// --- အရေးကြီး ---
// TODO: သင်၏ Firebase Project မှ Web App Configuration ကို ဤနေရာတွင် ထပ်မံထည့်သွင်းပါ။
// index.html မှာသုံးခဲ့တဲ့ config နဲ့ တူညီရပါမယ်။
const firebaseConfig = {
    apiKey: "YOUR_API_KEY",
    authDomain: "YOUR_PROJECT_ID.firebaseapp.com",
    projectId: "YOUR_PROJECT_ID",
    storageBucket: "YOUR_PROJECT_ID.appspot.com",
    messagingSenderId: "YOUR_MESSAGING_SENDER_ID",
    appId: "YOUR_APP_ID"
};

// Initialize the Firebase app in the service worker
firebase.initializeApp(firebaseConfig);

// Retrieve an instance of Firebase Messaging so that it can handle background messages.
const messaging = firebase.messaging();

// Handle background messages
messaging.onBackgroundMessage(function (payload) {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);

    // Customize the notification here
    const notificationTitle = payload.notification.title;
    const notificationOptions = {
        body: payload.notification.body,
        icon: '/firebase-logo.png' // Optional: Add an icon
    };

    self.registration.showNotification(notificationTitle, notificationOptions);
});
