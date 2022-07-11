using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using Zendesk.Chat;
using Zendesk.Messaging;

namespace ZendeskSample.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            var accountKey = "YwNKHLU2xtRO1k1NluWLVZj8nbneIPIi"; // your account key; 
            var appId = "5edba4bb00c08337fd9c7332998e75b7365a49e9a29ca971"; // your app id; 

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Chat.Instance.Init(this, accountKey, appId);
            var configuration = new ChatProvidersConfiguration.Builder().Build();

            Chat.Instance.ChatProvidersConfiguration = configuration;
            var alert = MessagingActivity.Builder()
                .WithEngines(ChatEngine.Engine());
            var config = ChatConfiguration.InvokeBuilder()
                .WithAgentAvailabilityEnabled(true)
                .WithOfflineFormEnabled(true)
                .WithPreChatFormEnabled(true)
                .WithChatMenuActions(ChatMenuAction.EndChat)
                .Build();

            alert.Show(this, config
            );

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

