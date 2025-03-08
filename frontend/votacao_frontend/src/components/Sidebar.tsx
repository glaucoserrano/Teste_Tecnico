import { Home, HowToVote, People, Restaurant } from "@mui/icons-material";
import { Drawer, List, ListItemButton, ListItemIcon, ListItemText, Toolbar } from "@mui/material";
import { ReactNode } from "react";
import { useNavigate } from "react-router-dom";

const menuItems = [
    {text: "Home", icon: <Home />, path:"/"},
    {text: "Usuários", icon: <People />, path:"/usuarios"},
    {text: "Restaurantes", icon: <Restaurant />, path:"/restaurantes"},
    {text: "Votação", icon: <HowToVote />, path:"/votacao"}
]

interface SidebarProps {
    children: ReactNode
}

const Sidebar = ({children} : SidebarProps) => {
    const  navigate = useNavigate();

    return(
        <div style={{display: "flex"}}>
            <Drawer variant="permanent" sx={{width: 240, flexShrink:0}}>
                <Toolbar />
                <List>
                    {menuItems.map((item) => (
                        <ListItemButton 
                        key={item.text}
                        onClick={() => navigate(item.path)}
                        >
                            <ListItemIcon>{item.icon}</ListItemIcon>
                            <ListItemText primary={item.text} />
                        </ListItemButton>
                    ))}
                </List>
            </Drawer>
            <main style={{ flexGrow: 1, padding: "16px", marginLeft:"240px"}}>
                {children}
            </main>
        </div>
    )
}
export default Sidebar