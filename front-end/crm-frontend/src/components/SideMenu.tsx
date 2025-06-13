import React from 'react';
import { 
    BiHome, 
    BiMessage , 
    BiSolidReport , 
    BiUserPlus , 
    BiTask  , 
    BiDollar , 
    BiUser , 
    BiBuildings,
    BiBookAlt
} from 'react-icons/bi';

import '../styles/sidemenu.css';

const  SideMenu = () =>{
  return (
    <>
      <div className='menu'>
        <div className='logo'>
            <BiBookAlt className='logo-icon'/>
            <h2>CRM</h2>
        </div>
        <div className='menu--list'>
            <a href='#' className='item'>
              <BiHome  className='icon'/>
                DashBoard
            </a>

            <a href='#' className='item'>
              <BiTask className='icon'/>
                Assigments
            </a>

            <a href='#' className='item'>
              <BiSolidReport className='icon'/>
                Reports
            </a>

            <a href='#' className='item'>
              <BiMessage className='icon' />
                Activities
            </a>

            <a href='#' className='item'>
              <BiUserPlus className='icon'/>
                Leads
            </a>

            <a href='#' className='item'>
              <BiDollar className='icon' />
                Deals
            </a>

             <a href='#' className='item'>
              <BiUser   className='icon'/>
                Customers
            </a>

              <a href='#' className='item'>
              <BiBuildings    className='icon'/>
                Companies
            </a>
        </div>
        
      </div>
      
     
    </>
  )
}

export default SideMenu
