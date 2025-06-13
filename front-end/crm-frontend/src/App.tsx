import './App.css'
import Content from './components/Content'
import SideMenu from './components/sidemenu'
import './App.css'

function App() {
  return (
    <>
      <div className='dashboard'>
        <SideMenu/>
        <div className='dashboard--content'>
          <Content />
        </div>
      </div>
    </>
  )
}

export default App
