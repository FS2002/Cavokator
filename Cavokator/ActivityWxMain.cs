﻿using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Cavokator
{
    [Activity(Label = "Cavokator", MainLauncher = true, Icon = "@drawable/ic_appicon",
     ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

    public class ActivityWxMain : AppCompatActivity
    {

        DrawerLayout drawerLayout;

        private SupportFragment mCurrentFragment;
        private WxMetarFragment mWxMetarFragment;
        private WxConditionFragment mWxConditionFragment;
        private Stack<SupportFragment> mStackFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.drawer_layout);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            mWxMetarFragment = new WxMetarFragment();
            mWxConditionFragment = new WxConditionFragment();

            mStackFragment = new Stack<SupportFragment>();


            // Initialize Toolbar
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.my_navigation_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Add fragments to container (FrameLayout)
            var ft = SupportFragmentManager.BeginTransaction(); 
            ft.Add(Resource.Id.flContent, mWxMetarFragment);
            ft.Commit();

            mCurrentFragment = mWxMetarFragment;

        }

        
        // Assess button pressed
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Hamburger button (open drawer)
            if (item.ItemId == Android.Resource.Id.Home)
                drawerLayout.OpenDrawer((int)GravityFlags.Start);
                return true;
        }


        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.action_fragment_metar:
                    ReplaceFragment(mWxMetarFragment);
                    break;
                case Resource.Id.action_fragment_condition:
                    ReplaceFragment(mWxConditionFragment);
                    break;
            }
            
            // Close drawer
            drawerLayout.CloseDrawers();
        }


        public void ReplaceFragment (SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var ft = SupportFragmentManager.BeginTransaction();

            ft.Replace(Resource.Id.flContent, fragment);
            ft.Commit();
            //ft.AddToBackStack(null);

            mCurrentFragment = fragment;
        }


        public override void OnBackPressed()
        {
            if (drawerLayout.IsDrawerOpen((int)GravityFlags.Start))
            {
                drawerLayout.CloseDrawers();
            }
            else
            {
                base.OnBackPressed();
            }

            

        }

    }

}