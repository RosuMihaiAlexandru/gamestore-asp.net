import React from 'react'
import { Outlet } from 'react-router-dom'
import { MainNav } from './MainNav';

export const MainLayout = () => {
  return (
    <>
    <MainNav />
    <main className='container'> 
    <Outlet/>
    </main>
    
    <footer className='container-fluid'>2023</footer>
    </>
  )
}
